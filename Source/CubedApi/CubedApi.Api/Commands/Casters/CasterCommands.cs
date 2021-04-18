using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities;
using System.Collections.Generic;
using System.Linq;
using CubedApi.Api.Data;
using System;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Common.Utilities.Interfaces;

namespace CubedApi.Api.Commands.Casters
{
    public class CasterCommands
    {
        private readonly SquidLeagueContext _context;
        private readonly IMapping _mapper;
        public CasterCommands(SquidLeagueContext context, IMapping mapping)
        {
            this._context = context ?? throw new ArgumentException("Context cannot be null.");
            this._mapper = mapping ?? throw new ArgumentException("Mapping cannot be null.");
        }

        /// <summary>
        /// Gets all active casters.
        /// </summary>
        /// <returns>A list of all casters.</returns>
        public List<CasterProfileDto> GetAllCasters()
        {
            var casters = this._context.CasterProfiles.Where(c => c.IsActive ?? false);
            if (casters.Count() == 0 || casters.IsNull())
            {
                throw new NoDataException();
            }

            return casters.Select(c => this._mapper.CasterProfileEntityToDto(c)).ToList();
        }


        /// <summary>
        /// Gets a caster profile by their Id.
        /// </summary>
        /// <param name="id">The id of the caster.</param>
        /// <returns>The caster with the related id.</returns>
        public CasterProfileDto GetCasterById(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var caster = this._context.CasterProfiles.FirstOrDefault(c => c.Id == id && (c.IsActive ?? false));
            if (caster.IsNull())
            {
                throw new NoDataException();
            }

            return this._mapper.CasterProfileEntityToDto(caster);
        }

        /// <summary>
        /// Gets the caster profile for a specific match id.
        /// </summary>
        /// <param name="id">The id of the match.</param>
        /// <returns>The caster for the specific match.</returns>
        public List<CasterProfileDto> GetCastersByMatchId(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var casters = this._context.CasterProfiles.Where(c => 
                (c.MatchCasterProfiles.Where(m => m.Id == id).Any()
                || c.MatchSecondaryCasterProfiles.Where(m => m.Id == id).Any())
                && (c.IsActive ?? false));
            if (!casters.Any())
            {
                throw new NoDataException();
            }

            return casters.Select(c => this._mapper.CasterProfileEntityToDto(c)).ToList();
        }
    }
}
