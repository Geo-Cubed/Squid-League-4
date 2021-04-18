using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities;
using CubedApi.Api.Common.Utilities.Interfaces;
using CubedApi.Api.Data;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Models.Linkers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CubedApi.Api.Commands.Matches
{
    public class MatchCommands
    {
        private readonly SquidLeagueContext _context;
        private readonly IMapping _mapper;

        public MatchCommands(SquidLeagueContext context, IMapping mapper)
        {
            this._context = context ?? throw new ArgumentException("Context cannot be null.");
            this._mapper = mapper ?? throw new ArgumentException("Mapper cannot be null.");
        }

        public List<MatchDto> GetAllSwissMatches()
        {
            var matches = this._context.Matches;
            if (!matches.Any())
            {
                throw new NoDataException();
            }

            return matches.Select(m => this._mapper.MatchEntityToDto(m)).ToList();
        }

        public MatchProfile GetMatchInformationById(int id)
        {
            // TODO: This command.
            throw new NotImplementedException();

            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var matchData = this._context.Matches;

            if (matchData.IsNull())
            {
                throw new DataIsNullException();
            }

            return null;
        }

        public List<UpcommingMatch> GetUpcommingMatches()
        {
            var matches = this._context.Matches
                .Where(m => m.MatchDate != null)
                .Select(m => this._mapper.MatchEntityToDto(m))
                .ToList();

            var upcomming = matches
                .Join(
                    this._context.Teams,
                    match => match.HomeTeamId,
                    team => team.Id,
                    (match, team) => new { match, team }
                )
                .Join(
                    this._context.Teams,
                    combined => combined.match.AwayTeamId,
                    awayTeam => awayTeam.Id,
                    (combined, awayTeam) => new UpcommingMatch() 
                        {
                            Match = combined.match,
                            HomeTeam = this._mapper.TeamEntityToDto(combined.team), 
                            AwayTeam = this._mapper.TeamEntityToDto(awayTeam)
                        }
                )
                .Where(m =>
                    (DateTime)m.Match.MatchDate > DateTime.UtcNow.Date
                    && (DateTime)m.Match.MatchDate <= DateTime.UtcNow.AddDays(8).AddSeconds(-1)
                )
                .ToList();

            if (!upcomming.Any())
            {
                throw new NoDataException();
            }

            return upcomming.ToList();
        }
    }
}
