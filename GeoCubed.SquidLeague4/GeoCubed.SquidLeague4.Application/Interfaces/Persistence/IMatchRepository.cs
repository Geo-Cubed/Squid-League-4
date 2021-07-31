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

        Task<IReadOnlyList<Match>> GetTeamPlayedMatches(int teamId);

        Task<bool> DoesMatchExist(int id);

        Task<string> GetStage(int id);
    }
}
