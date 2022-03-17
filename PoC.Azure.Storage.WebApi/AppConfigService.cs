using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PoC.Azure.Storage.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Azure.Storage.WebApi
{
    public class AppConfigService : IAppConfigService
    {
        public AppConfigService()
        {
        }
        private string _TableName = "applicationconfiguration";
        private readonly IConfiguration _configuration;
        public AppConfigService(IConfiguration configuration, string tableName)
        {
            _configuration = configuration;
            _TableName = tableName;
        }

        public async Task<AppConfigEntity> RetrieveAsync(string envName)
        {
            CloudStorageAccount account = new CloudStorageAccount(
    new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
        "<storage-accountname>",
        "<storage-accountkey>"), true);


            // Create the table client
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "items" table
            CloudTable table = client.GetTableReference("items");
            TableQuery<AppConfigEntity> itemStockQuery = new TableQuery<AppConfigEntity>().Where(TableQuery.GenerateFilterCondition("ConfigVal", QueryComparisons.Equal, envName));

            var rawMtlStock = table.ExecuteQuery(itemStockQuery);
            //var retrieveOperation = TableOperation.Retrieve<AppConfigEntity>(envName);
            //return await ExecuteTableOperation(retrieveOperation) as AppConfigEntity;
        }
        public async Task<AppConfigEntity> InsertOrMergeAsync(AppConfigEntity entity)
        {
            var insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
            return await ExecuteTableOperation(insertOrMergeOperation) as AppConfigEntity;
        }
        public async Task<AppConfigEntity> DeleteAsync(AppConfigEntity entity)
        {
            var deleteOperation = TableOperation.Delete(entity);
            return await ExecuteTableOperation(deleteOperation) as AppConfigEntity;
        }
        private async Task<object> ExecuteTableOperation(TableOperation tableOperation)
        {
            var table = await GetCloudTable();
            var tableResult = await table.ExecuteAsync(tableOperation);
            return tableResult.Result;
        }
        private async Task<CloudTable> GetCloudTable()
        {
            var storageAccount = CloudStorageAccount.Parse(_configuration["StorageConnectionString"]);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(TableName);
            await table.CreateIfNotExistsAsync();
            return table;
        }
    }
}
