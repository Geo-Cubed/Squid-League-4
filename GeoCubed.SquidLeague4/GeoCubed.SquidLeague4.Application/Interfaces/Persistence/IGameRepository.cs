using GeoCubed.SquidLeague4.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IGameRepository : IAsyncRepository<Game>
    {
        Task<IReadOnlyList<Game>> GetGamesByMatchId(int matchId);
        Task<IReadOnlyList<Game>> GetGamesByTeamId(int teamId);
        Task<IReadOnlyList<Game>> GetFullSetInfo(int matchId);
        Task<bool> DoesGameExist(int id);
    }
}
