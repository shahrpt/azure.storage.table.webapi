using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Azure.Storage.WebApi
{
    public class AzureTableStorage<T> : IAzureTableStorage<T>
       where T : AzureTableEntity, new()
    {
        #region " Public "

        public AzureTableStorage()
        {
            
        }
        private async Task<List<T>> GetListInner(string accountStorage, string evnName)
        {
            //Table
            CloudTable table = await GetTableAsync(accountStorage);

            //Query
            TableQuery<T> query = new TableQuery<T>()
                                        .Where(TableQuery.GenerateFilterCondition("ConfigVal",
                                                QueryComparisons.Equal, evnName));

            List<T> results = new List<T>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<T> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;

                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }
        
        public async Task<T> GetItem(long partitionKey, long rowKey)
        {
            return await GetItem(partitionKey.ToString(), rowKey.ToString());
        }

        public async Task<T> GetItem(string partitionKey, string rowKey)
        {
            //Table
            CloudTable table = await GetTableAsync("");

            //Operation
            TableOperation operation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            //Execute
            TableResult result = await table.ExecuteAsync(operation);

            return (T)(dynamic)result.Result;
        }

        public async Task Insert(T item)
        {
            //Table
            CloudTable table = await GetTableAsync("");

            //Operation
            TableOperation operation = TableOperation.Insert(item);

            //Execute
            await table.ExecuteAsync(operation);
        }

        public async Task Update(T item)
        {
            //Table
            CloudTable table = await GetTableAsync("");

            //Operation
            TableOperation operation = TableOperation.InsertOrReplace(item);

            //Execute
            await table.ExecuteAsync(operation);
        }

        public async Task Delete(string partitionKey, string rowKey)
        {
            //Item
            T item = await GetItem(partitionKey, rowKey);

            //Table
            CloudTable table = await GetTableAsync("");

            //Operation
            TableOperation operation = TableOperation.Delete(item);

            //Execute
            await table.ExecuteAsync(operation);
        }

        #endregion

        #region " Private "

        private readonly AzureTableSettings settings;

        private async Task<CloudTable> GetTableAsync(string accountStorage)
        {
            //Client
            //var StorageConnectionString = Configuration.GetSection("AzureTable:StorageConnectionString").Value,
            CloudTableClient tableClient = CloudStorageAccount.Parse(accountStorage).CreateCloudTableClient();

            //Table
            CloudTable table = tableClient.GetTableReference("applicationconfiguration");
            await table.CreateIfNotExistsAsync();

            return table;
        }


        public async Task<List<T>> GetList(string accountStorage, string evnName)
        {
            return await GetListInner(accountStorage, evnName);
        }

        #endregion
    }
}
