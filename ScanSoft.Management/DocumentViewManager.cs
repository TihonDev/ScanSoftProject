namespace ScanSoft.Management
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using ScanSoft.Models;
    using WindowsInput;
    using WindowsInput.Native;

    public class DocumentViewManager
    {
        private const double DEFAULT_ZOOM_PERCENTAGE = 25.5;
        private InputSimulator keyboardSimulator;

        public DocumentViewManager()
        {
            this.keyboardSimulator = new InputSimulator();
        }

        public void ShowDocumentInfo(DataGridView dataTable, Label docsCountLabel, ICollection<ScannedDocument> documentsCollection)
        {
            var docsInfo = documentsCollection
                    .Select(d => new { Name = d.Name, Type = d.Type, Description = d.Description, Created = d.DateOfCreation, Path_To_File = d.PathToFile })
                    .ToList();
            docsCountLabel.Text = $"Search results: {docsInfo.Count} documents";
            dataTable.DataSource = docsInfo;
        }

        public void ShowDocument(WebBrowser documentMonitor, string pathToFile)
        {
            var validator = new ValidationManager();
            if (validator.StringIsNullOrEmpty(pathToFile))
            {
                throw new ArgumentException("Invalid path to file location. Parameter name: pathToFile.");
            }

            documentMonitor.ScrollBarsEnabled = false;
            var filePath = pathToFile == "about:blank"
                ? pathToFile
                : $"{pathToFile}#zoom={DEFAULT_ZOOM_PERCENTAGE}%&navpanes=0&toolbar=0";
            documentMonitor.Navigate(filePath);
        }

        public void Zoom(VirtualKeyCode zoomActionKey)
        {
            this.keyboardSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            this.keyboardSimulator.Keyboard.KeyPress(zoomActionKey);
            this.keyboardSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
        }
    }
}
