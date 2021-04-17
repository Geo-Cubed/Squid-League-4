using CubedApi.CustomExceptions;
using CubedApi.Database.Repositories.Extentions;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepoFactory;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CubedApi.Api.Commands.Players
{
    public static class PlayerCommands
    {
        private static IRepository<Player> playerRepository;

        static PlayerCommands()
        {
            playerRepository = RepositoryFactory.GetPlayerRepository(RepoFactory.Enum.RepositoryTypes.Database);
        }

        /// <summary>
        /// Gets all players in the database that are active.
        /// </summary>
        /// <returns>A list of active players in the database.</returns>
        public static IEnumerable<Player> GetAllPlayers()
        {
            var players = playerRepository.GetItems();
            if (players.Count() == 0 || players.IsNull())
            {
                throw new NoDataException("No active players.");
            }

            return players;
        } 

        /// <summary>
        /// Gets a single active player info from their Id.
        /// </summary>
        /// <param name="id">The interger id of the player.</param>
        /// <returns>The player profile of the player.</returns>
        public static Player GetPlayerById(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var player = playerRepository.GetItem(id);
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
        public static  IEnumerable<Player> GetPlayersByTeamId(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var players = playerRepository.GetPlayersByTeamId(id);
            if (players.Count() == 0 || players.IsNull())
            {
                throw new NoDataException();
            }

            return players;
        }
    }
}
