using CubedApi.RepoFactory;
using CubedApi.RepositoryInterface;
using specialThanks = CubedApi.Models.DatabaseTables.HelpfulPeople;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CubedApi.CustomExceptions;
using System.Linq;

namespace CubedApi.BLL.HelpfulPeople
{
    public static class HelpfulPeopleCommands
    {
        private static readonly string connectionStr;
        private static readonly IRepository<specialThanks> helpfulPeopleRepository;

        static HelpfulPeopleCommands()
        {
            helpfulPeopleRepository = RepositoryFactory.GetHelpfulPeopleRepository(RepoFactory.Enum.RepositoryTypes.Database);
        }

        public static IEnumerable<specialThanks> GetAllHelpfulPeople()
        {
            var data = helpfulPeopleRepository.GetItems();
            if (data.Count() == 0)
            {
                throw new NoDataException();
            }

            return data;
        }
    }
}
