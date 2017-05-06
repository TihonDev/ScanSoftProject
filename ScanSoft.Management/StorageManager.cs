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
        private PdfReader docReader;
        private Twain32 scannedPages;

        public StorageManager(PdfReader documentReader, Twain32 scannedPages)
        {
            this.docReader = documentReader;
            this.scannedPages = scannedPages;
        }

        public void SaveDocument(string filename, string documentType, string docDescription, string archiveFolder, bool addToExistingFile, DatabaseManager databaseManager)
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
                this.SaveToFileSystem(filePath, addToExistingFile);
            }
        }

        public void SaveToFileSystem(string filePath, bool addToExistingFile)
        {
            var doc = new Document(PageSize.A4);
            doc.SetMargins(0, 0, 0, 0);
            var writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();
            if (addToExistingFile)
            {
                for (int pageNumber = 1; pageNumber <= this.docReader.NumberOfPages; pageNumber++)
                {
                    PdfImportedPage page = writer.GetImportedPage(this.docReader, pageNumber);
                    writer.DirectContent.AddTemplate(page, 0, 0);
                    doc.NewPage();
                }
            }

            for (int i = 0; i < this.scannedPages.ImageCount; i++)
            {
                Image pdfImage = Image.GetInstance(this.scannedPages.GetImage(i), System.Drawing.Imaging.ImageFormat.Jpeg);
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
