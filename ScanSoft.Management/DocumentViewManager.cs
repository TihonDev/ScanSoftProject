namespace ScanSoft.Management
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using ScanSoft.Models;

    public class DocumentViewManager
    {
        private int zoomPercentage;
        private string zoomFileLocation;
        private const int DEFAULT_ZOOM_PERCENTAGE = 25;
        private const int ZOOM_MAX_VALUE = 305;
        private const int ZOOM_MIN_VALUE = 5;

        public DocumentViewManager()
        {
            this.zoomPercentage = DEFAULT_ZOOM_PERCENTAGE;
        }

        public void ShowDocumentInfo(DataGridView dataTable, Label docsCountLabel, ICollection<ScannedDocument> documentsCollection)
        {
            var docsInfo = documentsCollection
                    .Select(d => new { Name = d.Name, Description = d.Description, Created = d.DateOfCreation, Path_To_File = d.PathToFile })
                    .ToList();
            docsCountLabel.Text = $"Search results: {docsInfo.Count} documents";
            dataTable.DataSource = docsInfo;
        }

        public void ShowDocument(WebBrowser documentMonitor, string pathToFile)
        {
            var validator = new ValidationManager();
            if (validator.StringIsNullOrEmpty(pathToFile))
            {
                throw new ArgumentException($"Invalid path to file location. Parameter name: pathToFile.");
            }

            var filePath = string.Empty;
            documentMonitor.ScrollBarsEnabled = false;
            if (pathToFile == "about:blank")
            {
                filePath = pathToFile;
                this.zoomPercentage = DEFAULT_ZOOM_PERCENTAGE;
                this.zoomFileLocation = string.Empty;
            }
            else
            {
                this.zoomFileLocation = pathToFile;
                filePath = $"{pathToFile}#zoom={this.zoomPercentage}%&navpanes=0&toolbar=0";
            }

            documentMonitor.Navigate(filePath);
        }

        public int ZoomIn(WebBrowser docDisplay)
        {
            if (this.zoomPercentage < ZOOM_MAX_VALUE)
            {
                this.zoomPercentage += 10;
                this.ShowDocument(docDisplay, this.zoomFileLocation);
            }
            return this.zoomPercentage;
        }

        public int ZoomOut(WebBrowser docDisplay)
        {
            if (zoomPercentage > ZOOM_MIN_VALUE)
            {
                this.zoomPercentage -= 10;
                this.ShowDocument(docDisplay, this.zoomFileLocation);
            }

            return this.zoomPercentage;
        }
    }
}
