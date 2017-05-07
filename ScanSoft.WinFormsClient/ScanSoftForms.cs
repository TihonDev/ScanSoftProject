namespace ScanSoft.WinFormsClient
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using iTextSharp.text.pdf;
    using ScanSoft.Management;
    using System.Linq;

    public partial class ScanSoftForms : Form
    {
        private bool openExistingFile;
        private PdfReader pdfReader;
        private string defaultFolder;
        private StorageManager storageAdmin;
        private ValidationManager validator;

        public ScanSoftForms()
        {
            this.InitializeComponent();
            this.Text = "ScanSoft";
            this.validator = new ValidationManager();
            this.storageAdmin = new StorageManager(this.pdfReader, this.twain322);
            this.defaultFolder = this.storageAdmin.SetArchiveDirectory();
            this.openExistingFile = false;

            if (!Directory.Exists("..\\..\\TempDocs"))
            {
                Directory.CreateDirectory("..\\..\\TempDocs");
            }
        }

        public static string TempFilePath
        {
            get
            {
                return Path.GetFullPath("..\\..\\TempDocs\\tempPdfDoc.pdf");
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "PDF|*.pdf";
            openDialog.InitialDirectory = this.defaultFolder;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string openedFileName = openDialog.SafeFileName;
                int extensionIndex = openedFileName.IndexOf(".pdf");
                this.fileNameTextBox.Text = openedFileName.Remove(extensionIndex);
                this.pdfReader = new PdfReader(openDialog.FileName);
                this.DisplayDocument(openDialog.FileName);
                this.openExistingFile = true;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var docInfo = this.validator.SaveDataValidation(this.twain322.ImageCount, this.fileNameTextBox.Text, this.docTypeComboBox.Text, this.docDescriptionTextBox.Text);
            if (!docInfo.isValid)
            {
                MessageBox.Show(docInfo.message);
            }
            else
            {
                storageAdmin.SaveDocument(this.fileNameTextBox.Text, this.docTypeComboBox.Text, this.docDescriptionTextBox.Text, this.defaultFolder, this.openExistingFile, new DatabaseManager());
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.openExistingFile = false;
            this.docTypeComboBox.Text = null;
            this.fileNameTextBox.Text = null;
            this.scannedPagesLabel.Text = "Scanned pages:";
            this.searchResultsLabel.Text = "Search results:";
            this.docDescriptionTextBox.Text = null;
            this.DisplayDocument("about:blank");
            File.Delete(TempFilePath);
            this.dataGridView1.DataSource = null;
        }

        private void Twain322_AcquireCompleted(object sender, EventArgs e)
        {
            File.Delete(TempFilePath);
            if (this.twain322.ImageCount > 0)
            {
                this.scannedPagesLabel.Text = string.Format("Scanned pages: {0}", this.twain322.ImageCount);
                storageAdmin.SaveToFileSystem(TempFilePath, false);
            }

            this.DisplayDocument(TempFilePath);
        }

        private void ScannersButton_Click(object sender, EventArgs e)
        {
            this.twain322.CloseDataSource();
            this.twain322.SelectSource();
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            this.twain322.Acquire();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var searchInfo = this.validator.SearchDataValidation(this.fileNameTextBox.Text, this.docTypeComboBox.Text);
            if (!searchInfo.isValid)
            {
                MessageBox.Show("Cannot search without filename! Please, input filename!");
            }
            else
            {
                var databaseManager = new DatabaseManager();
                var documentsFound = searchInfo.parameters == 1
                    ? databaseManager.GetDocuments(this.fileNameTextBox.Text)
                    : databaseManager.GetDocuments(this.fileNameTextBox.Text, this.docTypeComboBox.Text);

                var docsToPrint = documentsFound
                    .Select(d => new { Name = d.Name, Description = d.Description, Created = d.DateOfCreation, Path_To_File = d.PathToFile })
                    .ToList();
                this.searchResultsLabel.Text = $"Search results: {docsToPrint.Count} documents";
                this.dataGridView1.DataSource = docsToPrint;
            }
        }

        private void DisplayDocument(string path)
        {
            this.webPdfViewer.ScrollBarsEnabled = false;
            var pathToFile = path == "about:blank"
                ? path
                : $"{path}#toolbar=0&navpanes=0";
            this.webPdfViewer.Navigate(pathToFile);
        }

        private void ChangeDefaultFolderButton_Click(object sender, EventArgs e)
        {
            var changeDefaultFolderDialog = new FolderBrowserDialog();
            changeDefaultFolderDialog.Description = "Select default directory.";
            if (changeDefaultFolderDialog.ShowDialog() == DialogResult.OK)
            {
                this.defaultFolder = this.storageAdmin.ChangeArchiveDirectory(changeDefaultFolderDialog.SelectedPath);
            }
        }

        private void AxAcroPDFReader_Enter(object sender, EventArgs e)
        {
            this.DisplayDocument("doNotExists.pdf");
        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ScannedPagesLabel_Click(object sender, EventArgs e)
        {
        }

        private void FileNameTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void SearchResultsLabel_Click(object sender, EventArgs e)
        {
        }

        private void DocDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                this.DisplayDocument(this.dataGridView1.CurrentCell.Value.ToString());
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}