namespace ScanSoft.Models
{
    using System;

    public class ScannedDocument
    {
        public ScannedDocument()
        {
        }

        public ScannedDocument(string documentName, string description, string type, DateTime dateOfCreation, string pathToFile)
        {
            this.Name = documentName;
            this.Description = description;
            this.Type = type;
            this.DateOfCreation = dateOfCreation;
            this.PathToFile = pathToFile;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string PathToFile { get; set; }
    }
}
