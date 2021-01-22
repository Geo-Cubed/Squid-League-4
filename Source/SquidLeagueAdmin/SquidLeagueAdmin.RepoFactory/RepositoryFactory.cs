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

        public static IRepository<HelpfulPeople> GetHelpfulPersonRepository(string type)
        {
            IRepository<HelpfulPeople> repo = null;

            switch (type)
            {
                case "SQL":
                    repo = new DatabaseHelpfulPersonRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid helpful person repository type.");
            }

            return repo;
        }

        public static IRepository<Map> GetMapRepository(string type)
        {
            IRepository<Map> repo = null;

            switch (type)
            {
                case "SQL":
                    repo = new DatabaseMapRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid map repository type.");
            }

            return repo;
        }

        public static IRepository<Weapon> GetWeaponRepository(string type)
        {
            IRepository<Weapon> repo = null;

            switch (type)
            {
                case "SQL":
                    repo = new DatabaseWeaponRepository();
                    break;
                default:
                    throw new ArgumentException("Ivalid weapon repository type.");
            }

            return repo;
        }

        public static IRepository<Sub> GetSubRepository(string type)
        {
            IRepository<Sub> repo = null;

            switch (type)
            {
                case "SQL":
                    repo = new DatabaseSubRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid sub repository type.");
            }

            return repo;
        }

        public static IRepository<Special> GetSpecialRepository(string type)
        {
            IRepository<Special> repo = null;

            switch (type)
            {
                case "SQL":
                    repo = new DatabaseSpecialRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid special repository type.");
            }

            return repo;
        }

        public static IRepository<SystemSwitch> GetSystemSwitchRepository(string type)
        {
            IRepository<SystemSwitch> repo = null;

            switch (type)
            {
                case "SQL":
                    repo = new DatabaseSystemSwitchRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid system switch repository type");
            }

            return repo;
        }
    }
}
