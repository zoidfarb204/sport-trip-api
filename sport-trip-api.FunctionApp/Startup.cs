using sport_trip_api.FunctionApp;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.interfaces;

[assembly:FunctionsStartup(typeof(Startup))]
namespace sport_trip_api.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IBallparkRepository,BallparkRepository>();
        }
    }
}