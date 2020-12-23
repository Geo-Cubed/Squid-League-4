using CubedApi.Database.Repositories;
using CubedApi.Models.DatabaseTables;
using CubedApi.Models.ModelLinkers;
using CubedApi.RepositoryInterface;
using System;

namespace CubedApi.RepoFactory
{
    public static class RepositoryFactory
    {
        public static IRepository<Player> GetPlayerRepository(string type, string connectionStr = "")
        {
            IRepository<Player> repository = null;

            switch (type)
            {
                case "SQL":
                    repository = new PlayerRepository(connectionStr);
                    break;
                default:
                    throw new ArgumentException("Invalid player repository.");
            }

            return repository;
        }

        public static IRepository<Team> GetTeamRepository(string type, string connectionStr = "")
        {
            IRepository<Team> repository = null;

            switch (type)
            {
                case "SQL":
                    repository = new TeamRepository(connectionStr);
                    break;
                default:
                    throw new ArgumentException("Invalid team repository.");
            }

            return repository;
        }

        public static IRepository<TeamPlayers> GetTeamProfileRepository(string type, string connectionStr = "")
        {
            IRepository<TeamPlayers> repository = null;

            switch (type)
            {
                case "SQL":
                    repository = new TeamPlayerRepository(connectionStr);
                    break;
                default:
                    throw new ArgumentException("Invalid team profile repository");
            }

            return repository;
        }

        public static IRepository<CasterProfile> GetCasterRepository(string type, string connectionStr = "")
        {
            IRepository<CasterProfile> repository = null;

            switch (type)
            {
                case "SQL":
                    repository = new CasterRepository(connectionStr);
                    break;
                default:
                    throw new ArgumentException("Invalid caster repository");
            }

            return repository;
        }

        public static IRepository<HelpfulPeople> GetHelpfulPeopleRepository(string type, string connectionStr = "")
        {
            IRepository<HelpfulPeople> repository = null;

            switch (type)
            {
                case "SQL":
                    repository = new HelpfulPeopleRepository(connectionStr);
                    break;
                default:
                    throw new ArgumentException("Invalid helpful people repository");
            }

            return repository;
        }

        public static IRepository<Match> GetSwissMatchRepository(string type, string connectionStr = "")
        {
            IRepository<Match> repository = null;

            switch (type)
            {
                case "SQL":
                    repository = new SwissMatchRepository(connectionStr);
                    break;
                default:
                    throw new ArgumentException("Invalid swiss match repository");
            }

            return repository;
        }

        public static IRepository<SingleMatchInformation> GetSingleMatchRepository(string type, string connectionStr = "")
        {
            IRepository<SingleMatchInformation> repository = null;

            switch (type)
            {
                case "SQL":
                    repository = new SingleMatchRepository(connectionStr);
                    break;
                default:
                    throw new ArgumentException("Invalid single match repository");
            }

            return repository;
        }
    }
}
