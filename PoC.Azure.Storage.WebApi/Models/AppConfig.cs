using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Azure.Storage.WebApi.Models
{
    public class AppConfigEntity : AzureTableEntity
    {
        public AppConfigEntity()
        {
        }
        public AppConfigEntity(string PKey, string RKey)
        {
            PartitionKey = PKey;
            RowKey = RKey;
        }
         public string ConfigVal { get; set; }
       
    }
}
