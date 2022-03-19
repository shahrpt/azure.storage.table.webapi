using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoC.Azure.Storage.WebApi
{
    public interface IAzureTableStorage<T> where T : AzureTableEntity, new()
    {
        Task Delete(string partitionKey, string rowKey);
        Task<T> GetItem(long partitionKey, long rowKey);
        Task<T> GetItem(string partitionKey, string rowKey);
        Task<List<T>> GetList(string accountStorage, string evnName);
        Task Insert(T item);
        Task Update(T item);
    }
    public class AzureTableEntity : TableEntity
    {
    }
}