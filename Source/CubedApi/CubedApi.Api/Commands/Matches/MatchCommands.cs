using CubedApi.CustomExceptions;
using CubedApi.Models.DatabaseTables;
using CubedApi.Models.ModelLinkers;
using CubedApi.RepoFactory;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace CubedApi.Api.Commands.Matches
{
    public static class MatchCommands
    {
        private static readonly IRepository<Match> matchRepository;
        private static readonly IRepository<SingleMatchInformation> singleMatchRepository;

        static MatchCommands()
        {
            matchRepository = RepositoryFactory.GetSwissMatchRepository(RepoFactory.Enum.RepositoryTypes.Database);
            singleMatchRepository = RepositoryFactory.GetSingleMatchRepository(RepoFactory.Enum.RepositoryTypes.Database);
        }

        public static IEnumerable<Match> GetAllSwissMatches()
        {
            var matches = matchRepository.GetItems();
            if (matches.Count() == 0)
            {
                throw new NoDataException();
            }

            return matches;
        }

        public static SingleMatchInformation GetMatchInformationById(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var matchData = singleMatchRepository.GetItem(id);

            if (matchData.IsNull())
            {
                throw new DataIsNullException();
            }

            return matchData;
        }
    }
}
