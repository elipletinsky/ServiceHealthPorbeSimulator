using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HealthServiceSim
{
    public static class Service
    {
        private readonly static Random _random = new Random((int)DateTime.Now.Ticks);

        [FunctionName("Service")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "service/{service}/environment/{environment}/instance/{instance}")] HttpRequest req,
            string service, string environment, string instance,
            ILogger log)
        {
            var response = new { buildNumber = $"1.0.{_random.Next(1, 10)}", regionName = instance };
            var delay = TimeSpan.FromMilliseconds(_random.Next(100, 400));

            log.LogInformation($"Got request for {new { service, environment, instance }} - response {response} - delay {delay}");

            await Task.Delay(delay);
            return new OkObjectResult(response);
        }
    }
}
