namespace ScanSoft.Management
{
    using System.Collections.Generic;
    using System.Linq;
    using ScanSoft.Data;
    using ScanSoft.Models;

    public class DatabaseManager
    {
        public void InsertDocument(ScannedDocument scannedDoc)
        {
            var insertDocDbContext = new ScanSoftDbContext();
            using (insertDocDbContext)
            {
                insertDocDbContext.Documents.Add(scannedDoc);
                insertDocDbContext.SaveChanges();
            }
        }

        public ICollection<ScannedDocument> GetDocuments(string filename)
        {
            ICollection<ScannedDocument> result = null;
            var retrieveDocsContext = new ScanSoftDbContext();
            using (retrieveDocsContext)
            {
                result = retrieveDocsContext.Documents
                    .Where(d => d.Name.Contains(filename))
                    .ToList();
            }

            return result;
        }

        public ICollection<ScannedDocument> GetDocuments(string filename, string documentType)
        {
            ICollection<ScannedDocument> result = null;
            var retrieveDocsContext = new ScanSoftDbContext();
            using (retrieveDocsContext)
            {
                result = retrieveDocsContext.Documents
                        .Where(d => d.Name.Contains(filename) && d.Type == documentType)
                        .ToList();
            }

            return result;
        }

        public bool CheckForEqualFilename(string filename)
        {
            bool result = true;
            var checkFileNameDbContext = new ScanSoftDbContext();
            using (checkFileNameDbContext)
            {
                result = checkFileNameDbContext.Documents.Any(d => d.Name == filename);
            }

            return result;
        }
    }
}
