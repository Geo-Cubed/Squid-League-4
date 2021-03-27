using CubedApi.CustomExceptions;
using CubedApi.Database.Repositories.Extentions;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepoFactory;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CubedApi.BLL.Casters
{
    public static class CasterCommands
    {
        private static readonly string connectionStr;
        private static readonly IRepository<CasterProfile> casterRepository;

        static CasterCommands()
        {
            casterRepository = RepositoryFactory.GetCasterRepository(RepoFactory.Enum.RepositoryTypes.Database);
        }

        /// <summary>
        /// Gets all active casters.
        /// </summary>
        /// <returns>A list of all casters.</returns>
        public static IEnumerable<CasterProfile> GetAllCasters()
        {
            var casters = casterRepository.GetItems();
            if (casters.Count() == 0 || casters.IsNull())
            {
                throw new NoDataException();
            }

            return casters;
        }


        /// <summary>
        /// Gets a caster profile by their Id.
        /// </summary>
        /// <param name="id">The id of the caster.</param>
        /// <returns>The caster with the related id.</returns>
        public static CasterProfile GetCasterById(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var caster = casterRepository.GetItem(id);
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
        public static CasterProfile GetCasterByMatchId(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var caster = casterRepository.GetCasterByMatchId(id);
            if (caster.IsNull())
            {
                throw new NoDataException();
            }

            return caster;
        }
    }
}
