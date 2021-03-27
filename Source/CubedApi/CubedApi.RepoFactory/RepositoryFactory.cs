using CubedApi.Database;
using CubedApi.Database.Repositories;
using CubedApi.DatabaseInterface;
using CubedApi.Models.DatabaseTables;
using CubedApi.Models.ModelLinkers;
using CubedApi.RepoFactory.Enum;
using CubedApi.RepositoryInterface;
using System;
using System.IO;

namespace CubedApi.RepoFactory
{
    public static class RepositoryFactory
    {
        private static IDatabaseConnector connector;
        static RepositoryFactory()
        {
            // TODO: Make Class non static and move to DI where it hands out 1 version of repo factory whenever it requests an IRepoFactory
            var connectionStr = File.ReadAllText(@"D://connectionStr.txt");//"NOT FOR YOU YET GITHUB"; // TODO: Move this to a better place.
            connector = new DatabaseConnector(connectionStr);
        }

        public static IRepository<Player> GetPlayerRepository(RepositoryTypes type)
        {
            IRepository<Player> repository = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repository = new PlayerRepository(connector);
                    break;
                default:
                    throw new ArgumentException("Invalid player repository.");
            }

            return repository;
        }

        public static IRepository<Team> GetTeamRepository(RepositoryTypes type)
        {
            IRepository<Team> repository = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repository = new TeamRepository(connector);
                    break;
                default:
                    throw new ArgumentException("Invalid team repository.");
            }

            return repository;
        }

        public static IRepository<TeamPlayers> GetTeamProfileRepository(RepositoryTypes type)
        {
            IRepository<TeamPlayers> repository = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repository = new TeamPlayerRepository(connector);
                    break;
                default:
                    throw new ArgumentException("Invalid team profile repository");
            }

            return repository;
        }

        public static IRepository<CasterProfile> GetCasterRepository(RepositoryTypes type)
        {
            IRepository<CasterProfile> repository = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repository = new CasterRepository(connector);
                    break;
                default:
                    throw new ArgumentException("Invalid caster repository");
            }

            return repository;
        }

        public static IRepository<HelpfulPeople> GetHelpfulPeopleRepository(RepositoryTypes type)
        {
            IRepository<HelpfulPeople> repository = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repository = new HelpfulPeopleRepository(connector);
                    break;
                default:
                    throw new ArgumentException("Invalid helpful people repository");
            }

            return repository;
        }

        public static IRepository<Match> GetSwissMatchRepository(RepositoryTypes type)
        {
            IRepository<Match> repository = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repository = new SwissMatchRepository(connector);
                    break;
                default:
                    throw new ArgumentException("Invalid swiss match repository");
            }

            return repository;
        }

        public static IRepository<SingleMatchInformation> GetSingleMatchRepository(RepositoryTypes type)
        {
            IRepository<SingleMatchInformation> repository = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repository = new SingleMatchRepository(connector);
                    break;
                default:
                    throw new ArgumentException("Invalid single match repository");
            }

            return repository;
        }
    }
}
