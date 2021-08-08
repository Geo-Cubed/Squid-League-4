using GeoCubed.SquidLeague4.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IPlayerRepository : IAsyncRepository<Player>
    {
        Task<bool> DoesPlayerExist(int id);

        Task<IReadOnlyList<Player>> GetAllPlayersWithTeams();

        Task<Player> GetByIdWithTeam(int id);
        Task<IReadOnlyList<Player>> GetAllPlayersByTeamId(int teamId);
        Task<IReadOnlyList<Player>> GetPlayersWhoPlayed();
    }
}
