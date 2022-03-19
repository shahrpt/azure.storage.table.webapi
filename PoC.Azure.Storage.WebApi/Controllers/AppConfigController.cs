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
        public async Task<IList<AppConfigEntity>> Index()
        {
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _appConfigService.GetAllAsync(_userId);
        }
        
        // GET: AppConfig/Check
        public async Task<IActionResult> Check(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _appConfigService.CheckAsync(_userId, id.Value);
            }
            catch (Exception)
            {
                //Log error and notify user
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: AppConfig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _appConfigService.DeleteAsync(_userId, id);
            return RedirectToAction(nameof(Index));
        }
    }

}
