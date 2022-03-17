using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PoC.Azure.Storage.WebApi.Models;

namespace PoC.Azure.Storage.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppConfigController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AppConfigController> _logger;

        public AppConfigController(ILogger<AppConfigController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IEnumerable<AppConfigEntity> Get(string envName, string storage)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new AppConfigEntity
            {
                /*Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]*/
            })
            .ToArray();
        }
    }
}
