namespace ScanSoft.WinFormsClient
{
    using System;
    using System.Diagnostics;
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

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog()
            {
                Filter = "PDF|*.pdf",
                InitialDirectory = this.archiveFolder
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string openedFileName = openDialog.SafeFileName;
                int extensionIndex = openedFileName.IndexOf(".pdf");
                this.fileNameTextBox.Text = openedFileName.Remove(extensionIndex);
                this.pdfReader = new PdfReader(openDialog.FileName);
                this.docViewAdmin.ShowDocument(this.pdfDisplay, openDialog.FileName);
                this.addPagesToExistingDocument = true;
                this.EnableDocumentViewButtons(true, false);
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
            this.EnableDocumentViewButtons(false, false);
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
                MessageBox.Show("Please select scanning device.");
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
                var documentsFound = searchInfo.searchParameters == 1
                    ? databaseManager.GetDocuments(this.fileNameTextBox.Text)
                    : databaseManager.GetDocuments(this.fileNameTextBox.Text, this.docTypeComboBox.Text);
                this.docViewAdmin.ShowDocumentInfo(this.documentsInfoTable, this.searchResultsLabel, documentsFound);
            }
        }

        private void ChangeDefaultFolderButton_Click(object sender, EventArgs e)
        {
            var changeDefaultFolderDialog = new FolderBrowserDialog()
            {
                Description = "Select default directory."
            };
            if (changeDefaultFolderDialog.ShowDialog() == DialogResult.OK)
            {
                this.archiveFolder = this.storageAdmin.ChangeArchiveDirectory(changeDefaultFolderDialog.SelectedPath);
            }
        }

        private void DocumentsInfoTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var enableShowDocumentButton =
                this.documentsInfoTable.CurrentCell.ColumnIndex == 4 ? true : false;

            var enableZoomButtons =
                this.addPagesToExistingDocument == true ? true : false;

            this.EnableDocumentViewButtons(enableZoomButtons, enableShowDocumentButton);
        }

        private void ShowDocumentButton_Click(object sender, EventArgs e)
        {
            this.docViewAdmin.ShowDocument(this.pdfDisplay, this.documentsInfoTable.CurrentCell.Value.ToString());
            this.EnableDocumentViewButtons(true, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "ScanSoft";
            this.docViewAdmin = new DocumentViewManager();
            this.validationAdmin = new ValidationManager();
            this.storageAdmin = new StorageManager();
            this.archiveFolder = this.storageAdmin.SetArchiveDirectory();
            this.addPagesToExistingDocument = false;
            this.EnableDocumentViewButtons(false, false);

            this.zoomInBtn.TextAlign = ContentAlignment.MiddleCenter;
            this.zoomOutBtn.TextAlign = ContentAlignment.MiddleCenter;

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

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            var openExplorerInfo = new ProcessStartInfo()
            {
                FileName = "explorer.exe",
                Arguments = $"/select,{this.documentsInfoTable.CurrentCell.Value.ToString()}"
            };
            Process.Start(openExplorerInfo);
        }

        private void EnableDocumentViewButtons(bool zoomEnabled, bool showDocumentEnabled)
        {
            this.zoomInBtn.Enabled = zoomEnabled;
            this.zoomOutBtn.Enabled = zoomEnabled;
            this.openFolderBtn.Enabled = zoomEnabled;
            this.showDocumentButton.Enabled = showDocumentEnabled;
        }
    }
}