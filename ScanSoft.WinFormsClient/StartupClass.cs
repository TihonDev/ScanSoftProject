namespace ScanSoft.WinFormsClient
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public static class StartupClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ScanSoftForms());
            File.Delete(ScanSoftForms.TempFilePath);
        }
    }
}
