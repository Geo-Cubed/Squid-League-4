﻿using SquidLeagueAdmin.JSON.Repositories;
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
    }
}
