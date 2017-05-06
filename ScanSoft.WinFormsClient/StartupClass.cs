namespace ScanSoft.WinFormsClient
{
    using System;
    using System.Windows.Forms;
    using System.IO;

    public static class StartupClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            if (!Directory.Exists("..\\..\\TempDocs"))
            {
                Directory.CreateDirectory("..\\..\\TempDocs");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ScanSoftForms());
            File.Delete(ScanSoftForms.TempFilePath);
        }
    }
}
