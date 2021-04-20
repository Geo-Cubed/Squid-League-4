using CubedApi.Api.Data;
using CubedApi.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CubedApi.Api.Common.Utilities
{
    public static class PlayerEntityExtentions
    {
        public static List<Weapon> GetCommonWeapons(this Player player, SquidLeagueContext context)
        {
            return context.Weapons
                .ToList()
                .Join(
                    context.WeaponPlayeds,
                    weapon => weapon.Id,
                    played => played.WeaponId,
                    (weapon, played) => new { weapon, played }
                )
                .Join(
                    context.Players,
                    combined => combined.played.PlayerId,
                    dbPlayer => dbPlayer.Id,
                    (combined, dbPlayer) => new
                    {
                        PlayerId = dbPlayer.Id,
                        Weapon = combined.weapon
                    }
                )
                .Where(cmb => cmb.PlayerId == player.Id)
                .GroupBy(cmb => cmb.Weapon)
                .Select(group => new
                    {
                        Weapon = group.Key,
                        Count = group.Count()
                    }
                )
                .OrderByDescending(weps => weps.Count)
                .Take(3)
                .Select(wep => wep.Weapon)
                .ToList();
        }
    }
}
