namespace ScanSoft.WinFormsClient
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using iTextSharp.text.pdf;
    using ScanSoft.Management;
    using Saraff.Twain;

    public partial class ScanSoftForms : Form
    {
        private bool openExistingFile;
        private PdfReader pdfReader;
        private string archiveFolder;
        private StorageManager storageAdmin;
        private ValidationManager validator;
        private DocumentViewManager docViewAdmin;

        public ScanSoftForms()
        {
            this.InitializeComponent();
            this.Text = "ScanSoft";
            this.docViewAdmin = new DocumentViewManager();
            this.validator = new ValidationManager();
            this.storageAdmin = new StorageManager();
            this.archiveFolder = this.storageAdmin.SetArchiveDirectory();
            this.openExistingFile = false;
            this.showDocumentButton.Enabled = false;

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
            openDialog.InitialDirectory = this.archiveFolder;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string openedFileName = openDialog.SafeFileName;
                int extensionIndex = openedFileName.IndexOf(".pdf");
                this.fileNameTextBox.Text = openedFileName.Remove(extensionIndex);
                this.pdfReader = new PdfReader(openDialog.FileName);
                this.docViewAdmin.ShowDocument(this.pdfDisplay, openDialog.FileName);
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
                storageAdmin.SaveDocument(this.fileNameTextBox.Text, this.docTypeComboBox.Text, this.docDescriptionTextBox.Text, this.archiveFolder, this.openExistingFile, new DatabaseManager(), this.pdfReader, this.twain322);
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
            this.docViewAdmin.ShowDocument(this.pdfDisplay, "about:blank");
            this.documentsInfoTable.DataSource = null;
            this.showDocumentButton.Enabled = false;
        }

        private void Twain322_AcquireCompleted(object sender, EventArgs e)
        {
            File.Delete(TempFilePath);
            if (this.twain322.ImageCount > 0)
            {
                this.scannedPagesLabel.Text = string.Format("Scanned pages: {0}", this.twain322.ImageCount);
                storageAdmin.SaveToFileSystem(this.pdfReader, this.twain322, TempFilePath, false);
            }

            this.docViewAdmin.ShowDocument(this.pdfDisplay, TempFilePath);
        }

        private void ScannersButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.twain322.CloseDataSource();
                this.twain322.SelectSource();
            }
            catch (TwainException)
            {
                MessageBox.Show("Please select scanner.");
            }
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
                MessageBox.Show("Cannot search without filename! Please input filename!");
            }
            else
            {
                var databaseManager = new DatabaseManager();
                var documentsFound = searchInfo.parameters == 1
                    ? databaseManager.GetDocuments(this.fileNameTextBox.Text)
                    : databaseManager.GetDocuments(this.fileNameTextBox.Text, this.docTypeComboBox.Text);
                this.docViewAdmin.ShowDocumentInfo(this.documentsInfoTable, this.searchResultsLabel, documentsFound);
            }
        }

        private void ChangeDefaultFolderButton_Click(object sender, EventArgs e)
        {
            var changeDefaultFolderDialog = new FolderBrowserDialog();
            changeDefaultFolderDialog.Description = "Select default directory.";
            if (changeDefaultFolderDialog.ShowDialog() == DialogResult.OK)
            {
                this.archiveFolder = this.storageAdmin.ChangeArchiveDirectory(changeDefaultFolderDialog.SelectedPath);
            }
        }

        private void documentsInfoTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.documentsInfoTable.CurrentCell.ColumnIndex == 3)
            {
                this.showDocumentButton.Enabled = true;
            }
        }

        private void showDocumentButton_Click(object sender, EventArgs e)
        {
            this.docViewAdmin.ShowDocument(this.pdfDisplay, this.documentsInfoTable.CurrentCell.Value.ToString());
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



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }


    }
}