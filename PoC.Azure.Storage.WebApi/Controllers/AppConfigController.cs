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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace PoC.Azure.Storage.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppConfigController : ControllerBase
    {

        private readonly IAppConfigService _appConfigService;
        private IConfiguration _configuration;
        public AppConfigController(IAppConfigService appConfigService, IConfiguration Configuration)
        {
            _appConfigService = appConfigService;
            _configuration = Configuration;
        }

        // GET: AppConfig
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage">storage name</param>
        /// <param name="env">evnviroment name</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string storage, string env)
        {
            try
            {
                if (string.IsNullOrEmpty(storage) || string.IsNullOrEmpty(env))
                    return null;
                var storageConStr = _configuration[$"{storage.ToLower()}:connectionString"].ToString();

                var retVal = await _appConfigService.GetAllAppConfigsAsync(storageConStr, env);

                var jsonResult = JsonConvert.SerializeObject(retVal);

                if (jsonResult != null)
                {
                    return Ok(jsonResult);
                }
                return NotFound();
            }catch(Exception ex)
            {

                return null;
            }
        }
        
    }

}
