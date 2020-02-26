using FastMember;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Libs.Storage.Bulk
{
    public class BulkInserter
    {
        public Task InsertAsync<T>(IEnumerable<T> batch, string connectionString, params string[] members)
            => InsertAsync(batch, connectionString, new BulkOptions { ColumnNames = members });

        public Task InsertAsync<T>(IEnumerable<T> batch, DbTransaction transaction, params string[] members)
            => InsertAsync(batch, transaction, new BulkOptions { ColumnNames = members });

        public async Task InsertAsync<T>(IEnumerable<T> batch, string connectionString, BulkOptions options)
        {
            using var bulkCopy = new SqlBulkCopy(connectionString, options.SqlBulkCopyOptions);
            await InsertInternal(bulkCopy, batch, options);
        }

        public async Task InsertAsync<T>(IEnumerable<T> batch, DbTransaction transaction, BulkOptions options)
        {
            using var bulkCopy = new SqlBulkCopy((SqlConnection)transaction.Connection, options.SqlBulkCopyOptions, (SqlTransaction)transaction);
            await InsertInternal(bulkCopy, batch, options);
        }

        private async Task InsertInternal<T>(SqlBulkCopy bulkCopy, IEnumerable<T> batch, BulkOptions options)
        {
            if (options.ColumnNames is null)
            {
                throw new ArgumentNullException("Column names must be specified.", nameof(options.ColumnNames));
            }

            using var reader = ObjectReader.Create(batch, options.ColumnNames.ToArray());

            bulkCopy.DestinationTableName = options.TableName ?? $"{typeof(T).Name}s";
            bulkCopy.BatchSize = options.BatchCount ?? batch.Count();
            await bulkCopy.WriteToServerAsync(reader);
        }
    }
}
