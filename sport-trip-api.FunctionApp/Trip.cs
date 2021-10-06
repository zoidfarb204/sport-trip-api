using System;
using System.IO;
using System.Threading.Tasks;
using Common;
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
        
        [FunctionName("Park")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            return new OkObjectResult(await _ballparkRepo.GetBallparks());
        }
        
        [FunctionName("Trip")]
        public async Task<IActionResult> CreateTrip(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Trip/{name}")] HttpRequest req, ILogger log, string name)
        {
            return new OkObjectResult((await _ballparkRepo.ShortestPath(name)).ToString());
        }

    }
}