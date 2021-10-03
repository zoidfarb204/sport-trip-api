using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Repository.interfaces;

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
    }
}