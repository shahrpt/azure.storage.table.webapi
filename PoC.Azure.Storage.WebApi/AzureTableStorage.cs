using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Azure.Storage.WebApi
{
    public class AzureTableStorage<T> where T : IAzureTableStorage<T>, ITableEntity, new()
    {
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudTableClient tableClient;
        private readonly CloudTable table;

        public AzureTableStorage(string connectionString, string tableName)
        {
            // Retrieve the storage account from the connection string.
            storageAccount = CloudStorageAccount.Parse(connectionString);
            // Create the table client.
            //tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            table = tableClient.GetTableReference(tableName);

            // Create the table if it doesn't exist.
            table.CreateIfNotExistsAsync().GetAwaiter().GetResult();
        }

        public async Task<T> Get(Guid id)
        {
            //Please note: This assumes that the entity's id is a guid, and that our table is partitioned on the first character in the id.
            var rowKey = id.ToString();
            var partitionKey = rowKey.Substring(0, 1);

            TableQuery<T> query = new TableQuery<T>().Where(
            TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey))).Take(1);

            var results = new List<T>();

            TableContinuationToken continuationToken = null;
            do
            {
                var response = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = response.ContinuationToken;
                results.AddRange(response.Results);
            } while (continuationToken != null);

            return results.FirstOrDefault();
        }

        public async Task<List<T>> GetMany(TableQuery<T> query)
        {
            var results = new List<T>();

            TableContinuationToken continuationToken = null;
            do
            {
                var response = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = response.ContinuationToken;
                results.AddRange(response.Results);
            } while (continuationToken != null);

            return results;
        }

        public async Task<T> InsertOrUpdateAsync(T entity)
        {
            var insertOrReplaceOperation = TableOperation.InsertOrReplace(entity);
            var result = await table.ExecuteAsync(insertOrReplaceOperation);

            if (result == null || result.Result == null)
                return default(T);

            return (T)result.Result;
        }

        public async Task<T> Delete(T entity)
        {
            var deleteOperation = TableOperation.Delete(entity);
            var result = await table.ExecuteAsync(deleteOperation);

            return (T)result.Result;
        }

        public void BatchInsert(TableBatchOperation batchOperation)
        {
            table.ExecuteBatchAsync(batchOperation).GetAwaiter().GetResult();
        }
    }
}
