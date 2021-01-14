using SquidLeagueAdmin.JSON.Repositories;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Database.Repositories;
using System;

namespace SquidLeagueAdmin.RepoFactory
{
    public static class RepositoryFactory
    {
        // Do repo stuff here.
        public static IRepository<Config> GetConfigRepository(string type)
        {
            IRepository<Config> repo = null;
            
            switch (type)
            {
                case "JSON":
                    repo = new JsonConfigRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid config repository type used.");
            }

            return repo;
        }

        public static IRepository<Team> GetTeamRepository(string type)
        {
            IRepository<Team> repo = null;

            switch (type)
            {
                case "SQL":
                    repo = new DatabaseTeamRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid team repository used.");
            }

            return repo;
        }

        public static IRepository<Player> GetPlayerRepository(string type)
        {
            IRepository<Player> repo = null;

            switch (type)
            {
                case "SQL":
                    repo = new DatabasePlayerRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid player repository type.");
            }

            return repo;
        }

        public static IRepository<Caster> GetCasterRepository(string type)
        {
            IRepository<Caster> repo = null;

            switch (type)
            {
                case "SQL":
                    repo = new DatabaseCasterRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid caster repository type.");
            }

            return repo;
        }
    }
}
