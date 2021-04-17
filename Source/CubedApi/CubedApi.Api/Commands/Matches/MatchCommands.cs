using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities;
using CubedApi.Api.Data;
using CubedApi.Api.Models.Entities;
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

        public List<Match> GetAllSwissMatches()
        {
            var matches = this._context.Matches;
            if (!matches.Any())
            {
                throw new NoDataException();
            }

            return matches.ToList();
        }

        public Tuple<Match, List<Game>> GetMatchInformationById(int id)
        {
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
