﻿using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
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
            var gameSetting = this._dbContext.GameSettings.FirstOrDefault(g => g.Id == id);
            return Task.FromResult(gameSetting != null);
        }
    }
}