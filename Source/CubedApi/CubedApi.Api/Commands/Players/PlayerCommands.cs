using CubedApi.Api.Data;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using CubedApi.Api.Models.Entities;

namespace CubedApi.Api.Commands.Players
{
    public class PlayerCommands
    {
        private readonly SquidLeagueContext _context;

        public PlayerCommands(SquidLeagueContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Cannot have a null db context.");
            }

            this._context = context;
        }

        /// <summary>
        /// Gets all players in the database that are active.
        /// </summary>
        /// <returns>A list of active players in the database.</returns>
        public List<Player> GetAllPlayers()
        {
            var players = this._context.Players.Where(p => p.IsActive ?? false);
            if (players.Count() == 0 || players.IsNull())
            {
                throw new NoDataException("No active players.");
            }

            return players.ToList();
        } 

        /// <summary>
        /// Gets a single active player info from their Id.
        /// </summary>
        /// <param name="id">The interger id of the player.</param>
        /// <returns>The player profile of the player.</returns>
        public Player GetPlayerById(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var player = this._context.Players.FirstOrDefault(p => p.Id == id && (p.IsActive ?? false));
            if (player.IsNull())
            {
                throw new DataIsNullException();
            }

            return player;
        }

        /// <summary>
        /// Gets a list of active players on an active team.
        /// </summary>
        /// <param name="id">The id of the team.</param>
        /// <returns>A list of active players.</returns>
        public List<Player> GetPlayersByTeamId(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var players = this._context.Players.Where(p => p.TeamId == id && (p.IsActive ?? false));
            if (players.Count() == 0 || players.IsNull())
            {
                throw new NoDataException();
            }

            return players.ToList();
        }
    }
}
