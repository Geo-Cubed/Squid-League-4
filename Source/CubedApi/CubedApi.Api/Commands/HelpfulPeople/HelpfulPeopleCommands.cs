using System.Collections.Generic;
using System.Linq;
using CubedApi.Api.Data;
using System;
using CubedApi.Api.Models.Entities;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Common.Utilities;
using CubedApi.Api.Common.Utilities.Interfaces;

namespace CubedApi.Api.Commands.HelpfulPeople
{
    public class HelpfulPeopleCommands
    {
        private readonly SquidLeagueContext _context;
        private readonly IMapping _mapper;

        public HelpfulPeopleCommands(SquidLeagueContext context, IMapping mapper)
        {
            this._context = context ?? throw new ArgumentException("Context cannot be null.");
            this._mapper = mapper ?? throw new ArgumentException("Mapper cannot be null.");
        }

        public List<HelpfulPersonDto> GetAllHelpfulPeople()
        {
            var data = this._context.HelpfulPeople;
            if (!data.Any())
            {
                throw new NoDataException();
            }

            return data.Select(h => this._mapper.HelpfulPersonToDto(h)).ToList();
        }
    }
}
