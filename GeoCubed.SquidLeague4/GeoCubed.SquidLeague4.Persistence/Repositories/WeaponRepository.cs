using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class WeaponRepository : BaseRepository<Weapon>, IWeaponRepository
    {
        public WeaponRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesWeaponExist(int weaponId)
        {
            var weapons = this._dbContext.Weapons.AsNoTracking().Any(x => x.Id == weaponId);
            return Task.FromResult(weapons);
        }

        public Task<IReadOnlyList<Weapon>> GetAllWeaponsAndSubSpecials()
        {
           var weapons = this._dbContext.Weapons
                .OrderBy(w => w.WeaponName)
                .Include(w => w.WeaponSub)
                .Include(w => w.WeaponSpecial).ToList();
            return Task.FromResult((IReadOnlyList<Weapon>)weapons);
        }

        public Task<IReadOnlyList<Weapon>> GetPlayerWeapons(int playerId)
        {
            var weapons = this._dbContext.Weapons
                .ToList()
                .Join(
                    this._dbContext.WeaponPlayeds,
                    weapon => weapon.Id,
                    played => played.WeaponId,
                    (weapon, played) => new { weapon, played }
                )
                .Join(
                    this._dbContext.Players,
                    combined => combined.played.PlayerId,
                    dbPlayer => dbPlayer.Id,
                    (combined, dbPlayer) => new
                    {
                        PlayerId = dbPlayer.Id,
                        Weapon = combined.weapon
                    }
                )
                .Where(cmb => cmb.PlayerId == playerId)
                .GroupBy(cmb => cmb.Weapon)
                .Select(group => new
                {
                    Weapon = group.Key,
                    Count = group.Count()
                }
                )
                .OrderByDescending(weps => weps.Count)
                .Select(wep => wep.Weapon)
                .ToList();

            return Task.FromResult((IReadOnlyList<Weapon>)weapons);
        }
    }
}
