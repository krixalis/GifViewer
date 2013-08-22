using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GifViewer
{
    static class Images
    {
        private static string[] images      = null;
        private static string[] filesInPath = null;
        private static string activeImage   = null;
        private static string fileName      = null;
        private static string filePath      = null;
        private static int ID               = 0;

        public static string[] Array
        {
            get { return images; }
            set { images = value; }
        }

        public static string[] GetFilesInFolder(string pFilePath)
        {
            if (!IsFolder())
            {
                FilePath = pFilePath;
                FilesInPath = Directory.GetFiles(@FilePath);
            }
            else
            {
                FilesInPath = Directory.GetFiles(@pFilePath);
            }
            return FilesInPath;
        }

        public static bool IsFolder()
        {
            FileAttributes arg0 = File.GetAttributes(Array[0]);
            if ((arg0 & FileAttributes.Directory) == FileAttributes.Directory) { return true; }
            else { return false; }
        }

        public static int GetImageID(string[] pFilePool, string pFileName)
        {
            for(int i = 0; i < pFilePool.Length; i++)
            {
                if (pFilePool[i] == pFileName)
                {
                    ID = i;
                    return ID;  //Return the ID 'prematurely' if it was found.
                }
            }
            return -1;        //Return -1 if the ID was not found for some reason.
        }

        public static int GetImageID(string pFileName)  //Overload in case the folder is already known.
        {
            for (int i = 0; i < FilePath.Length; i++)
            {
                if (FilesInPath[i] == pFileName)
                {
                    ID = i;
                    return ID;  //Return the ID 'prematurely' if it was found.
                }
            }
            return -1;        //Return -1 if the ID was not found for some reason.
        }

        public static int GetCurrentID()
        {
            return ID;
        }



        public static void SetImageID(string pFileName)
        {
            if (FilesInPath[0] == null) GetFilesInFolder(ActiveImage);
            for (int i = 0; i < FilesInPath.Length; i++)
            {
                if (FilesInPath[i] == pFileName)
                {
                    ID = i;
                }
            }
        }

        public static string NextImage()
        {
            if (FilesInPath != null && FilesInPath.Length > ID + 1)
            {
                return FilesInPath[ID + 1];
            }
            else
            {
                return FilesInPath[0];
            }
        }

        public static string PrevImage()
        {
            if (FilesInPath != null && ID - 1 >= 0) 
            {
                return FilesInPath[ID-1];
            }
            else
            {
                return FilesInPath[FilesInPath.Length-1];
            }
        }

        public static string[] FilesInPath
        {
            get { return filesInPath; }
            set { filesInPath = value; }
        }

        public static string ActiveImage
        {
            get { return activeImage; }
            set { activeImage = value; }
        }

        public static string FileName
        {
            get { return fileName; }
            set { fileName = Path.GetFileName(value); }
        }

        public static string FilePath
        {
            get { return filePath; }
            set { filePath = Path.GetDirectoryName(value); }
        }
    }
}
