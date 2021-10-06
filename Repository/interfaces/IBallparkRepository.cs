using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Models;
using Data;

namespace Repository.interfaces
{
    public interface IBallparkRepository
    {
        Task<List<Feature>> GetBallparks(bool includeMinors = false);
        Task<Path> ShortestPath(string startingPoint, bool includeMinors = false);
    }
}