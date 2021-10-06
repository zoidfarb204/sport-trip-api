using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Common;
using Common.Models;
using Data;
using Repository.interfaces;
using Ballpark = Data.Ballpark;

namespace Repository
{
    public class BallparkRepository : IBallparkRepository
    {
        public async Task<List<Feature>> GetBallparks(bool includeMinors = false)
        {
            if (includeMinors == true)
            {
                return (await Ballpark.GetParks()).features;
            }

            return (await Ballpark.GetParks()).features.Where(x => x.properties.Class == "Majors").ToList();
        }

        public async Task<Path> ShortestPath(string startingPoint, bool includeMinors = false)
        {
            var parks = await GetBallparks(includeMinors);

            return new Routing(parks.Select(x => new Park
            {
                Lat = x.geometry.coordinates[0],
                Lng = x.geometry.coordinates[1],
                ParkName = x.properties.Ballpark
            }).ToList()).GetRoute(startingPoint);
        }
    }
}