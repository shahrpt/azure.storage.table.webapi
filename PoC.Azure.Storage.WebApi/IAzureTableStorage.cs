using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoC.Azure.Storage.WebApi
{
    public interface IAzureTableStorage<T> where T : ITableEntity, IAzureTableStorage<T>, new()
    {
        public Task<T> Get(Guid id);
        public Task<List<T>> GetMany(TableQuery<T> query);
        public Task<T> InsertOrUpdateAsync(T entity);
        public Task<T> Delete(T entity);
        public void BatchInsert(TableBatchOperation batchOperation);
    }
}