using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesGameExist(int id)
        {
            var game = this._dbContext.Games.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(game != null);
        }

        public Task<IReadOnlyList<Game>> GetGamesByMatchId(int matchId)
        {
            var games = this._dbContext.Games
                .Where(g => g.MatchId == matchId)
                .OrderByDescending(g => g.MatchId)
                .ThenBy(g => g.GameSetting.SortOrder)
                .Include(g => g.Match.HomeTeam)
                .Include(g => g.Match.AwayTeam)
                .Include(g => g.GameSetting.GameMap)
                .Include(g => g.GameSetting.GameMode)
                .Include(g => g.Match)
                .Include(g => g.WeaponPlayeds)
                    .ThenInclude(wp => wp.Player)
                .Include(g => g.WeaponPlayeds)
                    .ThenInclude(wp => wp.Weapon)
                .Include(g => g.WeaponPlayeds).ToList();

            return Task.FromResult((IReadOnlyList<Game>)games);
        }

        public Task<IReadOnlyList<Game>> GetGamesByTeamId(int teamId)
        {
            var games = this._dbContext.Games
                .Where(g => g.Match.HomeTeamId == teamId || g.Match.AwayTeamId == teamId)
                .OrderByDescending(g => g.MatchId)
                .ThenBy(g => g.GameSetting.SortOrder)
                .Include(g => g.Match.HomeTeam)
                .Include(g => g.Match.AwayTeam)
                .Include(g => g.GameSetting.GameMap)
                .Include(g => g.GameSetting.GameMode)
                .Include(g => g.Match)
                .Include(g => g.WeaponPlayeds)
                    .ThenInclude(wp => wp.Player)
                .Include(g => g.WeaponPlayeds)
                    .ThenInclude(wp => wp.Weapon)
                .ToList();

            return Task.FromResult((IReadOnlyList<Game>)games);
        }
    }
}
