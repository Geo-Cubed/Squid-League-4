using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class GameSettingRepository : BaseRepository<GameSetting>, IGameSettingRepository
    {
        public GameSettingRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesGameSettingExist(int id)
        {
            var gameSetting = this._dbContext.GameSettings.AsNoTracking().FirstOrDefault(g => g.Id == id);
            return Task.FromResult(gameSetting != null);
        }

        public Task<IReadOnlyList<GameSetting>> GetMapListByMatchId(int matchId)
        {
            var match = this._dbContext.Matches.AsNoTracking()
                .Include(x => x.BracketKnockouts)
                .Include(x => x.BracketSwisses)
                .Where(x => x.Id == matchId)
                .FirstOrDefault();

            var matchStage = this.GetStage(match);
            var gameSettings = this._dbContext.GameSettings
                .Include(x => x.GameMap)
                .Include(x => x.GameMode)
                .Where(x => x.BracketStage == matchStage)
                .OrderBy(x => x.SortOrder)
                .ToList();

            return Task.FromResult((IReadOnlyList<GameSetting>)gameSettings);
        }

        private string GetStage(Match match)
        {
            var value = string.Empty;
            if (match.BracketSwisses != null && match.BracketSwisses.Count > 0)
            {
                value = match.BracketSwisses.FirstOrDefault().MatchWeek.ToString();
            }

            if (match.BracketKnockouts != null && match.BracketKnockouts.Count > 0)
            {
                value = match.BracketKnockouts.FirstOrDefault().Stage;
            }

            return value;
        }

        public Task<IReadOnlyList<GameSetting>> GetMapLists()
        {
            var gameSettings = this._dbContext.GameSettings
                .Include(x => x.GameMap)
                .Include(x => x.GameMode)
                .ToList();
            return Task.FromResult((IReadOnlyList<GameSetting>)gameSettings);
        }
    }
}
