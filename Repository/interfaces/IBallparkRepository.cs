using System.Collections.Generic;
using System.Threading.Tasks;
using Data;

namespace Repository.interfaces
{
    public interface IBallparkRepository
    {
        Task<List<Feature>> GetBallparks(bool includeMinors = false);
    }
}