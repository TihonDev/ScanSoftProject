namespace ScanSoft.Management
{
    using System;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System.IO;
    using ScanSoft.Models;
    using System.Windows.Forms;
    using Saraff.Twain;

    public class StorageManager
    {
        private string defaultFolderFile;

        public StorageManager()
        {
            this.defaultFolderFile = "..\\..\\DocsDefaultDirectory.txt";
        }

        public void SaveDocument(string filename, string documentType, string docDescription, string archiveFolder, bool addToExistingFile, DatabaseManager databaseManager, PdfReader documentReader, Twain32 scannedPages)
        {
            var newFileDirectory = this.CreateDirectory(archiveFolder, documentType);
            var filePath = $"{newFileDirectory}\\{filename}.pdf";
            var dateOfCreation = DateTime.Now;
            var newDocument = new ScannedDocument(filename, docDescription, documentType, dateOfCreation, filePath);

            var dbManager = databaseManager;
            dbManager.InsertDocument(newDocument);

            var saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = newFileDirectory;
            saveDialog.Filter = "PDF File|*.pdf";
            saveDialog.FileName = filename;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                this.SaveToFileSystem(documentReader, scannedPages, filePath, addToExistingFile);
            }
        }

        public string SetArchiveDirectory()
        {
            if (!File.Exists(this.defaultFolderFile))
            {
                var docsSystemFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                File.WriteAllText(this.defaultFolderFile, docsSystemFolder);
            }

            var result = File.ReadAllText(this.defaultFolderFile);
            return result;
        }

        public string ChangeArchiveDirectory(string newDirectory)
        {
            File.WriteAllText(this.defaultFolderFile, newDirectory);
            var result = File.ReadAllText(this.defaultFolderFile);
            return result;
        }

        public void SaveToFileSystem(PdfReader documentReader, Twain32 scannedPages, string filePath, bool addToExistingFile)
        {
            var doc = new Document(PageSize.A4);
            doc.SetMargins(0, 0, 0, 0);
            var writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();
            if (addToExistingFile)
            {
                for (int pageNumber = 1; pageNumber <= documentReader.NumberOfPages; pageNumber++)
                {
                    PdfImportedPage page = writer.GetImportedPage(documentReader, pageNumber);
                    writer.DirectContent.AddTemplate(page, 0, 0);
                    doc.NewPage();
                }
            }

            for (int i = 0; i < scannedPages.ImageCount; i++)
            {
                Image pdfImage = Image.GetInstance(scannedPages.GetImage(i), System.Drawing.Imaging.ImageFormat.Jpeg);
                pdfImage.ScaleAbsolute(600f, 820f);
                doc.Add(pdfImage);
                doc.NewPage();
            }

            doc.Close();
            doc.Dispose();
        }

        private string CreateDirectory(string archiveDirectory, string docType)
        {
            var year = DateTime.Now.Year.ToString();
            var month = DateTime.Now.ToString("MMMM");
            var day = DateTime.Now.ToString("dd-MMM-yyyy");

            string result = $"{archiveDirectory}\\{docType}\\{year}\\{month}\\{day}";
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }

            return result;
        }
    }
}
