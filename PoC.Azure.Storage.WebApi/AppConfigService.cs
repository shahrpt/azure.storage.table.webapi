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
      
        private readonly IAzureTableStorage<AppConfigEntity> _repository;
        public AppConfigService()
        {
            _repository = new AzureTableStorage<AppConfigEntity>();
        }

        public Task<AppConfigEntity> GetDetailsAsync(string userId, long id)
        {
            throw new NotImplementedException();
        }

        public Task CheckAsync(string userId, long id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string userId, AppConfigEntity productInfo)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string userId, AppConfigEntity productInfo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string userId, long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppConfigEntity>> GetAllAppConfigsAsync(string accountStorage, string evnName)
        {
            return _repository.GetList(accountStorage, evnName);
        }

        public Task<AzureTableEntity> GetAppConfigAsync(string partionKey, string rowKey)
        {
            throw new NotImplementedException();
        }

        Task<List<AppConfigEntity>> IAppConfigService.GetAllAppConfigsAsync(string accountStorage, string evnName)
        {
            return _repository.GetList(accountStorage, evnName);
        }
    }
}
