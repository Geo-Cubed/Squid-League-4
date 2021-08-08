using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesPlayerExist(int id)
        {
            var player = this._dbContext.Players.AsNoTracking().Any(p => p.Id == id);
            return Task.FromResult(player);
        }

        public Task<IReadOnlyList<Player>> GetAllPlayersByTeamId(int teamId)
        {
            var players = this._dbContext.Players
                .Where(x => x.TeamId == teamId)
                .ToList();

            return Task.FromResult(players as IReadOnlyList<Player>);
        }

        public Task<IReadOnlyList<Player>> GetAllPlayersWithTeams()
        {
            var players = this._dbContext.Players.Include(p => p.Team).ToList();
            return Task.FromResult((IReadOnlyList<Player>)players);
        }

        public Task<Player> GetByIdWithTeam(int id)
        {
            var player = this._dbContext.Players.Where(p => p.Id == id).Include(p => p.Team).FirstOrDefault();
            return Task.FromResult(player);
        }

        public Task<IReadOnlyList<Player>> GetPlayersWhoPlayed()
        {
            var players = this._dbContext.Players
                .FromSqlRaw("select distinct p.* " +
                "from `player` as p " +
                "inner join `weapon_played` as wp on wp.`player_id` = p.`id`" +
                "order by p.`in_game_name`;").ToList();

            return Task.FromResult((IReadOnlyList<Player>)players);
        }
    }
}
