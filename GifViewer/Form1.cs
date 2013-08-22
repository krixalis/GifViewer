using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GifViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Initialize();

            if (Images.Array.Length != 0)
            {
                InitializeUsingImages();
            }
        }

        public void InitializeUsingImages()
        {
            if (Images.IsFolder())
            {
                Images.GetFilesInFolder(Images.Array[0]);
                Images.Array = Images.FilesInPath;
            }

            Graphics.FromImage(pictureBox1.Image).Clear(pictureBox1.BackColor);
            Bitmap LoadedImage = new Bitmap(Images.Array[0]);
            this.pictureBox1.Image = LoadedImage;

            #region Really gross way of setting window size to match Image size, change this!
            this.Width = LoadedImage.Width + 256;
            this.Height = LoadedImage.Height + 256;
            #endregion


            Images.ActiveImage = Images.Array[0];
            Images.FileName = Images.Array[0];
            Images.FilePath = Images.Array[0];
            Images.FilesInPath = Images.GetFilesInFolder(Images.ActiveImage);
            Images.SetImageID(Images.ActiveImage);

            this.Text = Images.FileName;
        }

        void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(ClientRectangle.Width, ClientRectangle.Height);
        }

        void Initialize()
        {
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            InitializeComponent();
            this.Text = "Krix Gif Viewer";
            Resize += Form1_Resize;
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;  //I couldn't be bothered having a file just to know what SizeMode the user likes, so I default to this.
            pictureBox1.AllowDrop = true;   //easily forgotten but necessary for DragnDrop features.
        }

        void UpdateImage()
        {
            Bitmap nextImage = new Bitmap(Images.ActiveImage);
            this.pictureBox1.Image = nextImage;
            Images.SetImageID(Images.ActiveImage);
            Images.FileName = Images.ActiveImage;
            this.Text = Images.FileName;
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) DisplayNextImage();
            else if (e.KeyCode == Keys.Left)  DisplayPrevImage();  
        }

        private void DisplayNextImage()
        {
            if (Images.NextImage() != "null")
            {
                Images.ActiveImage = Images.NextImage();
                UpdateImage();
            }
        }

        private void DisplayPrevImage()
        {
            if (Images.PrevImage() != "null")
            {
                Images.ActiveImage = Images.PrevImage();
                UpdateImage();
            }
        }

        #region ToolStrips
        #region View ToolStrip (PictureBoxSizeMode changes)
        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void stretchImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void autoSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void centerImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
        #endregion
        #region File ToolStrip
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(System.IO.File.Exists(Images.Array[Images.GetCurrentID()]))
            {
                System.IO.File.Delete(Images.Array[Images.GetCurrentID()]);
            }
        }
        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit application?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // The user wants to exit the application. Close everything down.
                Application.Exit();
            }

            
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            Images.Array = null;
            Images.Array = new string[256]; //gross, but gets IndexOutOfBounds when loading the dragndrop file otherwise.

            object[] obj = (object[])e.Data.GetData(DataFormats.FileDrop);

            string file = obj[0].ToString();
            
            Images.Array[0] = file;
            Images.Array = Images.GetFilesInFolder(file);
            Images.ActiveImage = file;
            UpdateImage();
        }

        private void pictureBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

    }
}
