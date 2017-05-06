namespace ScanSoft.Data
{
    using System.Data.Entity;
    using ScanSoft.Models;

    public class ScanSoftDbContext : DbContext
    {
        public ScanSoftDbContext()
            : base("DocumentsDBConnection")
        {
        }

        public virtual IDbSet<ScannedDocument> Documents { get; set; }
    }
}
