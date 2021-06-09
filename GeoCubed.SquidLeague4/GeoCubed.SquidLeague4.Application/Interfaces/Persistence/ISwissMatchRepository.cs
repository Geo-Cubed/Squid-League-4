using GeoCubed.SquidLeague4.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface ISwissMatchRepository : IAsyncRepository<BracketSwiss>
    {
        Task<bool> DoesSwissMatchExist(int id);
        Task<IReadOnlyList<BracketSwiss>> GetAllSwissMatchesWithMatches();
    }
}
