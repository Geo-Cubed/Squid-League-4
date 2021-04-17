using System.Collections.Generic;
using System.Linq;
using CubedApi.Api.Data;
using System;
using CubedApi.Api.Models.Entities;
using CubedApi.Api.Common.CustomExceptions;

namespace CubedApi.Api.Commands.HelpfulPeople
{
    public class HelpfulPeopleCommands
    {
        private readonly SquidLeagueContext _context;

        public HelpfulPeopleCommands(SquidLeagueContext context)
        {
            this._context = context ?? throw new ArgumentException("Context cannot be null.");
        }

        public List<HelpfulPerson> GetAllHelpfulPeople()
        {
            var data = this._context.HelpfulPeople;
            if (!data.Any())
            {
                throw new NoDataException();
            }

            return data.ToList();
        }
    }
}
