using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanSoft.Management
{
    public class ValidationManager
    {
        private DatabaseManager dbManager;

        public ValidationManager()
        {
            this.dbManager = new DatabaseManager();
        }

        public (bool isValid, string message) SaveDataValidation(int scannedPages, string filename, string docType, string description)
        {
            if (scannedPages == 0)
            {
                return (false, "You cannot save a blank document.");

            }

            bool incorrectFilename = this.StringIsNullOrEmpty(filename);
            bool incorrectDocType = this.StringIsNullOrEmpty(docType);
            bool incorrectDescription = this.StringIsNullOrEmpty(description);
            if (incorrectFilename || incorrectDocType || incorrectDescription)
            {
                return (false, "You cannot save the file without name, description or document type!");
            }

            bool hasEqualFilename = this.dbManager.CheckForEqualFilename(filename);
            if (hasEqualFilename)
            {
                return (false, "You already have a document with that name, please change the filename.");
            }

            return (true, string.Empty);
        }

        public (bool isValid, int parameters) SearchDataValidation(string filename, string docType)
        {
            var incorrectFilename = this.StringIsNullOrEmpty(filename);
            var incorrectDocType = this.StringIsNullOrEmpty(docType);
            if (incorrectFilename)
            {
                return (false, 0);
            }
            else
            {
                if (incorrectDocType)
                {
                    return (true, 1);
                }

                return (true, 2);
            }
        }

        public bool StringIsNullOrEmpty(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return true;
            }

            return false;
        }
    }
}
