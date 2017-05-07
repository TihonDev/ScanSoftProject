using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScanSoft.Models;

namespace ScanSoft.Management
{
    public class DocumentViewManager
    {
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
            documentMonitor.ScrollBarsEnabled = false;
            var filePath = pathToFile == "about:blank"
                ? pathToFile
                : $"{pathToFile}#toolbar=0&navpanes=0";
            documentMonitor.Navigate(filePath);
        }
    }
}
