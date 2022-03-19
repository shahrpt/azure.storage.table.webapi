using PoC.Azure.Storage.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Azure.Storage.WebApi
{
    /*public interface IAppConfigService
    {
        Task<IList<AppConfigEntity>> GetAllAsync(string userId);
        Task<AppConfigEntity> GetDetailsAsync(string userId, long id);
        Task CheckAsync(string userId, long id);
        Task AddAsync(string userId, AppConfigEntity productInfo);
        Task UpdateAsync(string userId, AppConfigEntity productInfo);
        Task DeleteAsync(string userId, long id);
    }*/
    public interface IAppConfigService
    {
        Task<List<AzureTableEntity>> GetAllAppConfigsAsync(string accountStorage, string evnName);
        Task<AzureTableEntity> GetAppConfigAsync(string partionKey, string rowKey);
        Task AddAsync(string productId, AzureTableEntity appConfig);
    }
}
