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
