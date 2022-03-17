using PoC.Azure.Storage.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Azure.Storage.WebApi
{
    public interface IAppConfigService
    {
        Task<AppConfigEntity> RetrieveAsync(string envName);
        Task<AppConfigEntity> InsertOrMergeAsync(AppConfigEntity entity);
        Task<AppConfigEntity> DeleteAsync(AppConfigEntity entity);
    }
}
