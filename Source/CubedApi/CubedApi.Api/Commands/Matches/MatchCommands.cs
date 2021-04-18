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

        public List<MatchDto> GetUpcommingMatches()
        {
            var matches = this._context.Matches
                .Where(m => m.MatchDate != null)
                .Select(m => this._mapper.MatchEntityToDto(m))
                .ToList();

            matches = matches.Where(m =>
                (DateTime)m.MatchDate > DateTime.UtcNow.Date
                && (DateTime)m.MatchDate <= DateTime.UtcNow.AddDays(8).AddSeconds(-1))
                .ToList();

            if (!matches.Any())
            {
                throw new NoDataException();
            }

            return matches;
        }
    }
}
