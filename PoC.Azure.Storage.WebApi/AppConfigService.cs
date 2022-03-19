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
      
        private readonly IConfiguration _configuration;
        private readonly IAzureTableStorage<AzureTableEntity> _repository;
        public AppConfigService(IConfiguration configuration, IAzureTableStorage<AzureTableEntity> repository)
        {
            _configuration = configuration;
            _repository = new AzureTableStorage<AzureTableEntity>();
        }
        public AppConfigService()
        {
            _repository = new AzureTableStorage<AzureTableEntity>();
        }

        public Task<AzureTableEntity> GetDetailsAsync(string userId, long id)
        {
            throw new NotImplementedException();
        }

        public Task CheckAsync(string userId, long id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string userId, AzureTableEntity productInfo)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string userId, AzureTableEntity productInfo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string userId, long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AzureTableEntity>> GetAllAppConfigsAsync(string accountStorage, string evnName)
        {
            return _repository.GetList(accountStorage, evnName);
        }

        public Task<AzureTableEntity> GetAppConfigAsync(string partionKey, string rowKey)
        {
            throw new NotImplementedException();
        }

        Task<List<AzureTableEntity>> IAppConfigService.GetAllAppConfigsAsync(string accountStorage, string evnName)
        {
            return _repository.GetList(accountStorage, evnName);
        }
    }
}
