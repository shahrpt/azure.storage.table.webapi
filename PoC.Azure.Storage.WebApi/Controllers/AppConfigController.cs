using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PoC.Azure.Storage.WebApi.Models;
using System.Security.Claims;

namespace PoC.Azure.Storage.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppConfigController : ControllerBase
    {

        private readonly IAppConfigService _appConfigService;

        public AppConfigController(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }

        // GET: AppConfig
        public async Task<IList<AzureTableEntity>> Index(string accStorage, string evnName)
        {
            if (string.IsNullOrEmpty(accStorage) || string.IsNullOrEmpty(evnName))
                return null;
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _appConfigService.GetAllAppConfigsAsync(accStorage, evnName);
        }
        
    }

}
