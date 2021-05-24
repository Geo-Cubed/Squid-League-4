using GeoCubed.SquidLeague4.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IMatchRepository : IAsyncRepository<Match>
    {
        Task<IReadOnlyList<Match>> GetUpcommingMatchesAsync();

        Task<IReadOnlyList<Match>> GetAllMatchesAsync();

        Task<Match> GetMatchById(int id);

        Task<bool> DoesMatchExist(int id);
    }
}
