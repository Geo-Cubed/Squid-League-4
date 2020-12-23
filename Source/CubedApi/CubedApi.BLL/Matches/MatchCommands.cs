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
namespace CubedApi.BLL.Matches
{
    public static class MatchCommands
    {
        private static readonly string connectionStr;
        private static readonly IRepository<Match> matchRepository;
        private static readonly IRepository<SingleMatchInformation> singleMatchRepository;

        static MatchCommands()
        {
            connectionStr = File.ReadAllText(@"D://connectionStr.txt");//"NOT FOR YOU YET GITHUB"; // TODO: Move this to a better place.
            matchRepository = RepositoryFactory.GetSwissMatchRepository("SQL", connectionStr);
            singleMatchRepository = RepositoryFactory.GetSingleMatchRepository("SQL", connectionStr);
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
