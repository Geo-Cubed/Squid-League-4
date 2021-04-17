using CubedApi.CustomExceptions;
using CubedApi.Database.Repositories.Extentions;
using CubedApi.Models.DatabaseTables;
using CubedApi.Models.ModelLinkers;
using CubedApi.RepoFactory;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CubedApi.Api.Commands.Teams
{
    public static class TeamCommands
    {
        private static readonly IRepository<Team> teamRepository;
        private static readonly IRepository<TeamPlayers> teamProfileRepository;

        static TeamCommands()
        {
            teamRepository = RepositoryFactory.GetTeamRepository(RepoFactory.Enum.RepositoryTypes.Database);
            teamProfileRepository = RepositoryFactory.GetTeamProfileRepository(RepoFactory.Enum.RepositoryTypes.Database);
        }

        /// <summary>
        /// Gets all active teams
        /// </summary>
        /// <returns>All active teams in the database</returns>
        public static IEnumerable<Team> GetAllTeams()
        {
            var teams = teamRepository.GetItems();
            if (teams.Count() == 0 || teams.IsNull())
            {
                throw new NoDataException();
            }

            return teams;
        }

        /// <summary>
        /// Gets active team by its id
        /// </summary>
        /// <param name="id">The id of the team to fetch</param>
        /// <returns>The team with the id</returns>
        public static Team GetTeamById(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var team = teamRepository.GetItem(id);
            if (team.IsNull())
            {
                throw new DataIsNullException();
            }

            return team;
        }

        /// <summary>
        /// Gets the team that a certain player is on by their id
        /// </summary>
        /// <param name="id">id of the player to get the team of</param>
        /// <returns>the team the player is on</returns>
        public static Team GetTeamByPlayerId(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var team = teamRepository.GetTeamByPlayerId(id);
            if (team.IsNull())
            {
                throw new DataIsNullException();
            }

            return team;
        }

        /// <summary>
        /// gets all the teams with the players on it
        /// </summary>
        /// <returns>a list of teams with players</returns>
        public static IEnumerable<TeamPlayers> GetTeamProfiles()
        {
            var teams = teamProfileRepository.GetItems();
            if (teams.Count() == 0)
            {
                throw new NoDataException();
            }

            return teams;
        }
    }
}
