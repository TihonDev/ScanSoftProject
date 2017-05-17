namespace ScanSoft.WinFormsClient
{
    public partial class ScanSoftForms
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button openFileBtn;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.ComboBox docTypeComboBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button clearButton;
        private Saraff.Twain.Twain32 twain322;
        private System.Windows.Forms.Button scannersButton;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.Label scannedPagesLabel;
        private System.Windows.Forms.Label filenameLabel;
        private System.Windows.Forms.Label typeOfDocumentLabel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label searchResultsLabel;
        private System.Windows.Forms.RichTextBox docDescriptionTextBox;
        private System.Windows.Forms.Label docDescriptionLabel;
        private System.Windows.Forms.Button changeDefaultFolderButton;
        private System.Windows.Forms.DataGridView documentsInfoTable;
        private System.Windows.Forms.WebBrowser pdfDisplay;
        private System.Windows.Forms.Button showDocumentButton;
        private System.Windows.Forms.Button zoomInBtn;
        private System.Windows.Forms.Button zoomOutBtn;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileBtn = new System.Windows.Forms.Button();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.docTypeComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.twain322 = new Saraff.Twain.Twain32(this.components);
            this.scannersButton = new System.Windows.Forms.Button();
            this.scanButton = new System.Windows.Forms.Button();
            this.scannedPagesLabel = new System.Windows.Forms.Label();
            this.filenameLabel = new System.Windows.Forms.Label();
            this.typeOfDocumentLabel = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchResultsLabel = new System.Windows.Forms.Label();
            this.docDescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.docDescriptionLabel = new System.Windows.Forms.Label();
            this.changeDefaultFolderButton = new System.Windows.Forms.Button();
            this.documentsInfoTable = new System.Windows.Forms.DataGridView();
            this.pdfDisplay = new System.Windows.Forms.WebBrowser();
            this.showDocumentButton = new System.Windows.Forms.Button();
            this.zoomInBtn = new System.Windows.Forms.Button();
            this.zoomOutBtn = new System.Windows.Forms.Button();
            this.openFolderBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.documentsInfoTable)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileBtn
            // 
            this.openFileBtn.Location = new System.Drawing.Point(12, 12);
            this.openFileBtn.Name = "openFileBtn";
            this.openFileBtn.Size = new System.Drawing.Size(75, 23);
            this.openFileBtn.TabIndex = 0;
            this.openFileBtn.Text = "Open";
            this.openFileBtn.UseVisualStyleBackColor = true;
            this.openFileBtn.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(12, 118);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(156, 20);
            this.fileNameTextBox.TabIndex = 2;
            // 
            // docTypeComboBox
            // 
            this.docTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.docTypeComboBox.FormattingEnabled = true;
            this.docTypeComboBox.Items.AddRange(new object[] {
            "Certificate",
            "Contract",
            "Invoice",
            "Other"});
            this.docTypeComboBox.Location = new System.Drawing.Point(12, 186);
            this.docTypeComboBox.MaxDropDownItems = 10;
            this.docTypeComboBox.Name = "docTypeComboBox";
            this.docTypeComboBox.Size = new System.Drawing.Size(156, 21);
            this.docTypeComboBox.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 243);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(12, 538);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 5;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // twain322
            // 
            this.twain322.AppProductName = "Saraff.Twain.NET";
            this.twain322.Parent = null;
            this.twain322.AcquireCompleted += new System.EventHandler(this.Twain322_AcquireCompleted);
            // 
            // scannersButton
            // 
            this.scannersButton.Location = new System.Drawing.Point(228, 12);
            this.scannersButton.Name = "scannersButton";
            this.scannersButton.Size = new System.Drawing.Size(102, 23);
            this.scannersButton.TabIndex = 6;
            this.scannersButton.Text = "Scanners";
            this.scannersButton.UseVisualStyleBackColor = true;
            this.scannersButton.Click += new System.EventHandler(this.ScannersButton_Click);
            // 
            // scanButton
            // 
            this.scanButton.Location = new System.Drawing.Point(228, 41);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(102, 23);
            this.scanButton.TabIndex = 7;
            this.scanButton.Text = "Scan";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // scannedPagesLabel
            // 
            this.scannedPagesLabel.AutoSize = true;
            this.scannedPagesLabel.Location = new System.Drawing.Point(404, 12);
            this.scannedPagesLabel.Name = "scannedPagesLabel";
            this.scannedPagesLabel.Size = new System.Drawing.Size(85, 13);
            this.scannedPagesLabel.TabIndex = 8;
            this.scannedPagesLabel.Text = "Scanned pages:";
            // 
            // filenameLabel
            // 
            this.filenameLabel.AutoSize = true;
            this.filenameLabel.Location = new System.Drawing.Point(9, 102);
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Size = new System.Drawing.Size(55, 13);
            this.filenameLabel.TabIndex = 9;
            this.filenameLabel.Text = "Filename :";
            // 
            // typeOfDocumentLabel
            // 
            this.typeOfDocumentLabel.AutoSize = true;
            this.typeOfDocumentLabel.Location = new System.Drawing.Point(9, 170);
            this.typeOfDocumentLabel.Name = "typeOfDocumentLabel";
            this.typeOfDocumentLabel.Size = new System.Drawing.Size(99, 13);
            this.typeOfDocumentLabel.TabIndex = 10;
            this.typeOfDocumentLabel.Text = "Type of document :";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(93, 243);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 11;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // searchResultsLabel
            // 
            this.searchResultsLabel.AutoSize = true;
            this.searchResultsLabel.Location = new System.Drawing.Point(225, 380);
            this.searchResultsLabel.Name = "searchResultsLabel";
            this.searchResultsLabel.Size = new System.Drawing.Size(77, 13);
            this.searchResultsLabel.TabIndex = 13;
            this.searchResultsLabel.Text = "Search results:";
            // 
            // docDescriptionTextBox
            // 
            this.docDescriptionTextBox.Location = new System.Drawing.Point(228, 118);
            this.docDescriptionTextBox.Name = "docDescriptionTextBox";
            this.docDescriptionTextBox.Size = new System.Drawing.Size(261, 148);
            this.docDescriptionTextBox.TabIndex = 14;
            this.docDescriptionTextBox.Text = "";
            // 
            // docDescriptionLabel
            // 
            this.docDescriptionLabel.AutoSize = true;
            this.docDescriptionLabel.Location = new System.Drawing.Point(225, 102);
            this.docDescriptionLabel.Name = "docDescriptionLabel";
            this.docDescriptionLabel.Size = new System.Drawing.Size(110, 13);
            this.docDescriptionLabel.TabIndex = 15;
            this.docDescriptionLabel.Text = "Document description";
            // 
            // changeDefaultFolderButton
            // 
            this.changeDefaultFolderButton.Location = new System.Drawing.Point(12, 497);
            this.changeDefaultFolderButton.Name = "changeDefaultFolderButton";
            this.changeDefaultFolderButton.Size = new System.Drawing.Size(121, 23);
            this.changeDefaultFolderButton.TabIndex = 16;
            this.changeDefaultFolderButton.Text = "Change archive folder";
            this.changeDefaultFolderButton.UseVisualStyleBackColor = true;
            this.changeDefaultFolderButton.Click += new System.EventHandler(this.ChangeDefaultFolderButton_Click);
            // 
            // documentsInfoTable
            // 
            this.documentsInfoTable.AllowUserToAddRows = false;
            this.documentsInfoTable.AllowUserToDeleteRows = false;
            this.documentsInfoTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.documentsInfoTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.documentsInfoTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.documentsInfoTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.documentsInfoTable.Location = new System.Drawing.Point(228, 404);
            this.documentsInfoTable.Name = "documentsInfoTable";
            this.documentsInfoTable.ReadOnly = true;
            this.documentsInfoTable.RowHeadersVisible = false;
            this.documentsInfoTable.Size = new System.Drawing.Size(537, 157);
            this.documentsInfoTable.TabIndex = 18;
            this.documentsInfoTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DocumentsInfoTable_CellContentClick);
            // 
            // pdfDisplay
            // 
            this.pdfDisplay.Location = new System.Drawing.Point(515, 12);
            this.pdfDisplay.MinimumSize = new System.Drawing.Size(20, 20);
            this.pdfDisplay.Name = "pdfDisplay";
            this.pdfDisplay.ScrollBarsEnabled = false;
            this.pdfDisplay.Size = new System.Drawing.Size(250, 297);
            this.pdfDisplay.TabIndex = 19;
            this.pdfDisplay.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.PdfDisplay_DocumentCompleted);
            // 
            // showDocumentButton
            // 
            this.showDocumentButton.Location = new System.Drawing.Point(667, 375);
            this.showDocumentButton.Name = "showDocumentButton";
            this.showDocumentButton.Size = new System.Drawing.Size(98, 23);
            this.showDocumentButton.TabIndex = 20;
            this.showDocumentButton.Text = "Show Document";
            this.showDocumentButton.UseVisualStyleBackColor = true;
            this.showDocumentButton.Click += new System.EventHandler(this.ShowDocumentButton_Click);
            // 
            // zoomInBtn
            // 
            this.zoomInBtn.Location = new System.Drawing.Point(694, 315);
            this.zoomInBtn.Name = "zoomInBtn";
            this.zoomInBtn.Size = new System.Drawing.Size(71, 23);
            this.zoomInBtn.TabIndex = 21;
            this.zoomInBtn.Text = "zoom in";
            this.zoomInBtn.UseVisualStyleBackColor = true;
            this.zoomInBtn.Click += new System.EventHandler(this.ZoomInBtn_Click);
            // 
            // zoomOutBtn
            // 
            this.zoomOutBtn.Location = new System.Drawing.Point(515, 315);
            this.zoomOutBtn.Name = "zoomOutBtn";
            this.zoomOutBtn.Size = new System.Drawing.Size(71, 23);
            this.zoomOutBtn.TabIndex = 22;
            this.zoomOutBtn.Text = "zoom out";
            this.zoomOutBtn.UseVisualStyleBackColor = true;
            this.zoomOutBtn.Click += new System.EventHandler(this.ZoomOutBtn_Click);
            // 
            // openFolderBtn
            // 
            this.openFolderBtn.Location = new System.Drawing.Point(515, 375);
            this.openFolderBtn.Name = "openFolderBtn";
            this.openFolderBtn.Size = new System.Drawing.Size(92, 23);
            this.openFolderBtn.TabIndex = 23;
            this.openFolderBtn.Text = "Open Folder";
            this.openFolderBtn.UseVisualStyleBackColor = true;
            this.openFolderBtn.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // ScanSoftForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 573);
            this.Controls.Add(this.openFolderBtn);
            this.Controls.Add(this.zoomOutBtn);
            this.Controls.Add(this.zoomInBtn);
            this.Controls.Add(this.showDocumentButton);
            this.Controls.Add(this.pdfDisplay);
            this.Controls.Add(this.documentsInfoTable);
            this.Controls.Add(this.changeDefaultFolderButton);
            this.Controls.Add(this.docDescriptionLabel);
            this.Controls.Add(this.docDescriptionTextBox);
            this.Controls.Add(this.searchResultsLabel);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.typeOfDocumentLabel);
            this.Controls.Add(this.filenameLabel);
            this.Controls.Add(this.scannedPagesLabel);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.scannersButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.docTypeComboBox);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.openFileBtn);
            this.Name = "ScanSoftForms";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.documentsInfoTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Button openFolderBtn;
    }
}