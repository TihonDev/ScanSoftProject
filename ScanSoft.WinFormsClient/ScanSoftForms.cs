namespace ScanSoft.WinFormsClient
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using iTextSharp.text.pdf;
    using Saraff.Twain;
    using ScanSoft.Management;
    using WindowsInput.Native;

    public partial class ScanSoftForms : Form
    {
        private bool addPagesToExistingDocument;
        private string archiveFolder;
        private PdfReader pdfReader;
        private StorageManager storageAdmin;
        private ValidationManager validationAdmin;
        private DocumentViewManager docViewAdmin;

        public ScanSoftForms()
        {
            this.InitializeComponent();
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
                this.addPagesToExistingDocument = true;
                this.zoomInBtn.Visible = true;
                this.zoomOutBtn.Visible = true;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var docInfo = this.validationAdmin.SaveDataValidation(this.twain322.ImageCount, this.fileNameTextBox.Text, this.docTypeComboBox.Text, this.docDescriptionTextBox.Text);
            if (!docInfo.isValid)
            {
                MessageBox.Show(docInfo.message);
            }
            else
            {
                this.storageAdmin.SaveDocument(this.fileNameTextBox.Text, this.docTypeComboBox.Text, this.docDescriptionTextBox.Text, this.archiveFolder, this.addPagesToExistingDocument, new DatabaseManager(), this.pdfReader, this.twain322);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.addPagesToExistingDocument = false;
            this.docTypeComboBox.Text = null;
            this.fileNameTextBox.Text = null;
            this.scannedPagesLabel.Text = "Scanned pages:";
            this.searchResultsLabel.Text = "Search results:";
            this.docDescriptionTextBox.Text = null;
            this.docViewAdmin.ShowDocument(this.pdfDisplay, "about:blank");
            this.documentsInfoTable.DataSource = null;
            this.showDocumentButton.Enabled = false;
            this.zoomInBtn.Visible = false;
            this.zoomOutBtn.Visible = false;
        }

        private void Twain322_AcquireCompleted(object sender, EventArgs e)
        {
            File.Delete(TempFilePath);
            if (this.twain322.ImageCount > 0)
            {
                this.scannedPagesLabel.Text = string.Format("Scanned pages: {0}", this.twain322.ImageCount);
                this.storageAdmin.SaveToFileSystem(this.pdfReader, this.twain322, TempFilePath, false);
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
            var searchInfo = this.validationAdmin.SearchDataValidation(this.fileNameTextBox.Text, this.docTypeComboBox.Text);
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

        private void DocumentsInfoTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.showDocumentButton.Enabled =
                this.documentsInfoTable.CurrentCell.ColumnIndex == 3 ? true : false;
        }

        private void ShowDocumentButton_Click(object sender, EventArgs e)
        {
            this.docViewAdmin.ShowDocument(this.pdfDisplay, this.documentsInfoTable.CurrentCell.Value.ToString());
            this.zoomOutBtn.Visible = true;
            this.zoomInBtn.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "ScanSoft";
            this.docViewAdmin = new DocumentViewManager();
            this.validationAdmin = new ValidationManager();
            this.storageAdmin = new StorageManager();
            this.archiveFolder = this.storageAdmin.SetArchiveDirectory();
            this.addPagesToExistingDocument = false;
            this.showDocumentButton.Enabled = false;

            this.zoomInBtn.TextAlign = ContentAlignment.MiddleCenter;
            this.zoomOutBtn.TextAlign = ContentAlignment.MiddleCenter;
            this.zoomInBtn.Visible = false;
            this.zoomOutBtn.Visible = false;

            if (!Directory.Exists("..\\..\\TempDocs"))
            {
                Directory.CreateDirectory("..\\..\\TempDocs");
            }
        }

        private void PdfDisplay_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.pdfDisplay.Select();
        }

        private void ZoomOutBtn_Click(object sender, EventArgs e)
        {
            this.pdfDisplay.Select();
            this.docViewAdmin.Zoom(VirtualKeyCode.SUBTRACT);
        }

        private void ZoomInBtn_Click(object sender, EventArgs e)
        {
            this.pdfDisplay.Select();
            this.docViewAdmin.Zoom(VirtualKeyCode.ADD);
        }
    }
}