using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities;
using CubedApi.Api.Data;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Models.Entities;
using CubedApi.Api.Models.Linkers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CubedApi.Api.Commands.Matches
{
    public class MatchCommands
    {
        private readonly SquidLeagueContext _context;

        public MatchCommands(SquidLeagueContext context)
        {
            this._context = context ?? throw new ArgumentException("Context cannot be null.");
        }

        public List<MatchDto> GetAllSwissMatches()
        {
            var matches = this._context.Matches;
            if (!matches.Any())
            {
                throw new NoDataException();
            }

            return matches.Select(m => EntityDtoConverter.MatchEntityToDto(m)).ToList();
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
    }
}
