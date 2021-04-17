using System.Collections.Generic;
using System.Linq;
using CubedApi.Api.Data;
using System;
using CubedApi.Api.Models.Entities;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Common.Utilities;

namespace CubedApi.Api.Commands.HelpfulPeople
{
    public class HelpfulPeopleCommands
    {
        private readonly SquidLeagueContext _context;

        public HelpfulPeopleCommands(SquidLeagueContext context)
        {
            this._context = context ?? throw new ArgumentException("Context cannot be null.");
        }

        public List<HelpfulPersonDto> GetAllHelpfulPeople()
        {
            var data = this._context.HelpfulPeople;
            if (!data.Any())
            {
                throw new NoDataException();
            }

            return data.Select(h => EntityDtoConverter.HelpfulPersonToDto(h)).ToList();
        }
    }
}
