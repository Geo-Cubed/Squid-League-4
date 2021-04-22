using CubedApi.Api.Data;
using CubedApi.Api.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CubedApi.Api.Models.Entities;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Models.Linkers;
using CubedApi.Api.Common.Utilities.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CubedApi.Api.Commands.Teams
{
    public class TeamCommands
    {
        private readonly SquidLeagueContext _context;
        private readonly IMapping _mapper;

        public TeamCommands(SquidLeagueContext context, IMapping mapper)
        {
            this._context = context ?? throw new ArgumentException("Cannot have a null db context.");
            this._mapper = mapper ?? throw new ArgumentException("Cannot have a null mapper.");
        }

        /// <summary>
        /// Gets all active teams
        /// </summary>
        /// <returns>All active teams in the database</returns>
        internal List<TeamDto> GetAllTeams()
        {
            var teams = this._context.Teams.Where(t => t.IsActive ?? false);
            if (!teams.Any())
            {
                throw new NoDataException();
            }

            return teams.Select(t => this._mapper.TeamEntityToDto(t)).ToList();
        }

        /// <summary>
        /// Gets active team by its id
        /// </summary>
        /// <param name="id">The id of the team to fetch</param>
        /// <returns>The team with the id</returns>
        internal TeamDto GetTeamById(int id)
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

            return this._mapper.TeamEntityToDto(team);
        }

        /// <summary>
        /// Gets the team that a certain player is on by their id
        /// </summary>
        /// <param name="id">id of the player to get the team of</param>
        /// <returns>the team the player is on</returns>
        internal TeamDto GetTeamByPlayerId(int id)
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

            return this._mapper.TeamEntityToDto(team);
        }

        /// <summary>
        /// gets all the teams with the players on it
        /// </summary>
        /// <returns>a list of teams with players</returns>
        internal List<TeamProfile> GetTeamProfiles()
        {
            var teams = this._context.Teams.Where(t => t.IsActive ?? false).ToList();
            if (!teams.Any())
            {
                throw new NoDataException();
            }

            var teamPlayers = new List<TeamProfile>();
            foreach (var team in teams)
            {
                var players = this._context.Players.Where(p => (int)p.TeamId == team.Id && (p.IsActive ?? false)).ToList();
                if (players.Any())
                {
                    teamPlayers.Add(new TeamProfile()
                    {
                        team = this._mapper.TeamEntityToDto(team),
                        players = players.Select(p => this._mapper.PlayerEntityToDto(p)).ToList()
                    });
                }
            }
            return teamPlayers;
        }

        internal List<TeamResult> GetTeamResults(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var matches = this._context.Matches
                .Join(
                    this._context.Teams,
                    match => match.HomeTeamId,
                    homeTeam => homeTeam.Id,
                    (match, homeTeam) => new { match, homeTeam }
                )
                .Join(
                    this._context.Teams,
                    combined => combined.match.AwayTeamId,
                    awayTeam => awayTeam.Id,
                    (combined, awayTeam) => new { combined.match, combined.homeTeam, awayTeam }
                )
                .Where(x => 
                    (x.awayTeam.Id == id || x.homeTeam.Id == id) 
                    && (x.match.HomeTeamScore + x.match.AwayTeamScore > 0)
                )
                .OrderByDescending(x => x.match.MatchDate)
                .Select(x => 
                    new TeamResult() 
                    { 
                        MatchId = x.match.Id,
                        HomeTeamScore = (int)x.match.HomeTeamScore,
                        HomeTeamName = x.homeTeam.TeamName,
                        AwayTeamScore = (int)x.match.AwayTeamScore,
                        AwayTeamName = x.awayTeam.TeamName
                    }    
                )
                .ToList();
            if (!matches.Any())
            {
                throw new NoDataException();
            }

            return matches;
        }
    }
}
