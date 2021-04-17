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
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null.");
            }

            this._context = context;
        }

        public List<HelpfulPerson> GetAllHelpfulPeople()
        {
            var data = this._context.HelpfulPeople;
            if (data.Count() == 0)
            {
                throw new NoDataException();
            }

            return data;
        }
    }
}
