using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesTeamExist(int id)
        {
            var team = this._dbContext.Teams.Any(t => t.Id == id);
            return Task.FromResult(team);
        }

        public Task<IReadOnlyList<Team>> GetAllTeamsWithPlayers()
        {
            var teams = this._dbContext.Teams
                .Where(t => (bool)t.IsActive)
                .Include(t => t.Players)
                .ToList();
            return Task.FromResult((IReadOnlyList<Team>)teams);
        }
    }
}
