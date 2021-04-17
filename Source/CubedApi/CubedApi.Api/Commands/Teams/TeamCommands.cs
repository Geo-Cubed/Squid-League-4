using CubedApi.Api.Data;
using CubedApi.Api.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CubedApi.Api.Models.Entities;
using CubedApi.Api.Common.CustomExceptions;

namespace CubedApi.Api.Commands.Teams
{
    public class TeamCommands
    {
        private readonly SquidLeagueContext _context;

        public TeamCommands(SquidLeagueContext context)
        {
            this._context = context ?? throw new ArgumentException("Cannot have a null db context.");
        }

        /// <summary>
        /// Gets all active teams
        /// </summary>
        /// <returns>All active teams in the database</returns>
        public List<Team> GetAllTeams()
        {
            var teams = this._context.Teams.Where(t => t.IsActive ?? false);
            if (!teams.Any())
            {
                throw new NoDataException();
            }

            return teams.ToList();
        }

        /// <summary>
        /// Gets active team by its id
        /// </summary>
        /// <param name="id">The id of the team to fetch</param>
        /// <returns>The team with the id</returns>
        public Team GetTeamById(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var team = this._context.Teams.FirstOrDefault(t => t.Id == id && (t.IsActive ?? false));
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
        public Team GetTeamByPlayerId(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var team = this._context.Teams.FirstOrDefault(t => t.Players.Where(p => p.Id == id && (p.IsActive ?? false)).Any());
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
        public List<Tuple<Team, List<Player>>> GetTeamProfiles()
        {
            var teams = this._context.Teams.Where(t => t.IsActive ?? false);
            if (!teams.Any())
            {
                throw new NoDataException();
            }

            var teamPlayers = new List<Tuple<Team, List<Player>>>();
            foreach (var team in teams)
            {
                var players = this._context.Players.Where(p => (int)p.TeamId == team.Id && (p.IsActive ?? false));
                if (players.Any())
                {
                    teamPlayers.Add(new Tuple<Team, List<Player>>(team, players.ToList()));
                }
            }
            return teamPlayers;
        }
    }
}
