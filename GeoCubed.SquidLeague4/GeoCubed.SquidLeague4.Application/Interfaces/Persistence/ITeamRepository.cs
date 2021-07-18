using GeoCubed.SquidLeague4.Domain.Entities;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface ITeamRepository : IAsyncRepository<Team>
    {
        Task<bool> DoesTeamExist(int id);

        Task<Team> GetTeamWithPlayers(int TeamId);
    }
}
