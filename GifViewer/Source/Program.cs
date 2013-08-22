using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GifViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {

            #region Credit to http://stackoverflow.com/questions/6486195/ensuring-only-one-application-instance
            bool firstInstance;
            var mutex = new System.Threading.Mutex(true, "UniqueAppId", out firstInstance);


            if (!firstInstance)
            {
                return;
            }

            #endregion
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Images.Array = args;
            Application.Run(new Form1());

            GC.KeepAlive(mutex);
        }
    }
}