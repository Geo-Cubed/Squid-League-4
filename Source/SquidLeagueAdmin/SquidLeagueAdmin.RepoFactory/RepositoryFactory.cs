using SquidLeagueAdmin.JSON.Repositories;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using System;

namespace SquidLeagueAdmin.RepoFactory
{
    public static class RepositoryFactory
    {
        // Store the connection string for the database.
        static readonly string connectionStr;

        static RepositoryFactory()
        {
            // TODO: Get the connection string for the database.
        }

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
    }
}
