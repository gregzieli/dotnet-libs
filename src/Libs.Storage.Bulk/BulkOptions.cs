using System.Collections.Generic;
using System.Data.SqlClient;

namespace Libs.Storage.Bulk
{
    public class BulkOptions
    {
        public string TableName { get; set; }

        public IEnumerable<string> ColumnNames { get; set; }

        public int? BatchCount { get; set; }

        public SqlBulkCopyOptions SqlBulkCopyOptions { get; set; }
    }
}
