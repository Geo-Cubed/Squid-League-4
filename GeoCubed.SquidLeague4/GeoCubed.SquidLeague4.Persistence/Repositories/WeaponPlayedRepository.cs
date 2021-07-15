using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class WeaponPlayedRepository : BaseRepository<WeaponPlayed>, IWeaponPlayedRepository
    {
        public WeaponPlayedRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public async Task AddPlayersToGame(List<WeaponPlayed> playerWeapons)
        {
            if (playerWeapons == null || playerWeapons.Count == 0)
            {
                return;
            }

            // Delete old players.
            var gameId = playerWeapons.FirstOrDefault().GameId;
            var isHomeTeam = playerWeapons.FirstOrDefault().IsHomeTeam;
            var oldPlayers = this._dbContext.WeaponPlayeds
                .AsNoTracking()
                .Where(x => x.GameId == gameId && x.IsHomeTeam == isHomeTeam)
                .ToList();
            foreach (var player in oldPlayers)
            {
                await this.DeleteAsync(player);
            }

            // Insert the new players.
            await this._dbContext.WeaponPlayeds.AddRangeAsync(playerWeapons);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeletePlayersByGameId(int gameId)
        {
            var players = this._dbContext.WeaponPlayeds
                .Where(x => x.GameId == gameId)
                .ToList();

            foreach (var weaponPlayed in players)
            {
                var success = await this.DeleteAsync(weaponPlayed);
                if (!success)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
