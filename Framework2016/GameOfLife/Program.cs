using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CreateDirectory();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void CreateDirectory()
        {
            string path = Environment.CurrentDirectory;

            if (!Directory.Exists(path+"\\plugins"))
            {
                Directory.CreateDirectory(path + "\\plugins");
            }
        }
    }
}
