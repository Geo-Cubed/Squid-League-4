using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities;
using System.Collections.Generic;
using System.Linq;
using CubedApi.Api.Data;
using System;
using CubedApi.Api.Models.Entities;

namespace CubedApi.Api.Commands.Casters
{
    public class CasterCommands
    {
        private readonly SquidLeagueContext _context;

        public CasterCommands(SquidLeagueContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null.");
            }

            this._context = context;
        }

        /// <summary>
        /// Gets all active casters.
        /// </summary>
        /// <returns>A list of all casters.</returns>
        public List<CasterProfile> GetAllCasters()
        {
            var casters = this._context.CasterProfiles.Where(c => c.IsActive ?? false);
            if (casters.Count() == 0 || casters.IsNull())
            {
                throw new NoDataException();
            }

            return casters.ToList();
        }


        /// <summary>
        /// Gets a caster profile by their Id.
        /// </summary>
        /// <param name="id">The id of the caster.</param>
        /// <returns>The caster with the related id.</returns>
        public CasterProfile GetCasterById(int id)
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

            return caster;
        }

        /// <summary>
        /// Gets the caster profile for a specific match id.
        /// </summary>
        /// <param name="id">The id of the match.</param>
        /// <returns>The caster for the specific match.</returns>
        public List<CasterProfile> GetCastersByMatchId(int id)
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

            return casters.ToList();
        }
    }
}
