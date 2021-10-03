using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository;
using Repository.interfaces;

namespace sport_trip_api.FunctionApp
{
    public class Trip
    {
        private IBallparkRepository _ballparkRepo;

        public Trip(IBallparkRepository ballparkRepo)
        {
            _ballparkRepo = ballparkRepo;
        }
        
        [FunctionName("Trip")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(await _ballparkRepo.GetBallparks());
        }
    }
}