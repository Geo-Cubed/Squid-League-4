using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetSetInfo;
using GeoCubed.SquidLeague4.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GeoCubed.SquidLeague4.Application.Common.Helpers
{
    public static class CustomMapper
    {
        /// <summary>
        /// Converts a list of basic player weapons to the domain entity.
        /// </summary>
        /// <param name="playerWeapons">The list of player weapons.</param>
        /// <param name="gameId">The id of the game.</param>
        /// <param name="isHomeTeam">If the players were part of the home team.</param>
        /// <returns>A list of <see cref="WeaponPlayed"/> entities.</returns>
        public static List<WeaponPlayed> ConvertToWeaponPlayed(List<BasicPlayerWeapon> playerWeapons, int gameId, bool isHomeTeam)
        {
            var convertedList = playerWeapons
                .Select(x => new WeaponPlayed() 
                {
                    GameId = gameId,
                    IsHomeTeam = isHomeTeam,
                    PlayerId = x.PlayerId,
                    WeaponId = x.WeaponId
                })
                .ToList();

            return convertedList;
        }
    }
}
