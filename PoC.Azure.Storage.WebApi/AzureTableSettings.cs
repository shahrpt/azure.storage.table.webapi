using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Azure.Storage.WebApi
{
    public class AzureTableSettings
    {
        public AzureTableSettings(string storageConnectionString, string tableName)
        {
            if (string.IsNullOrEmpty(storageConnectionString))
            {
                throw new ArgumentNullException("StorageConnectionString");
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("TableName");
            }

            StorageConnectionString = storageConnectionString;
            TableName = tableName;
        }

        public string TableName { get; }
        public string StorageConnectionString { get; set; }
    }
}
