using SquidLeagueAdmin.JSON.Repositories;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Database.Repositories;
using System;
using SquidLeagueAdmin.Models.Enums;

namespace SquidLeagueAdmin.RepoFactory
{
    public static class RepositoryFactory
    {
        // Do repo stuff here.
        public static IRepository<Config> GetConfigRepository(RepositoryTypes type)
        {
            IRepository<Config> repo = null;
            
            switch (type)
            {
                case RepositoryTypes.Json:
                    repo = new JsonConfigRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid config repository type used.");
            }

            return repo;
        }

        public static IRepository<Team> GetTeamRepository(RepositoryTypes type)
        {
            IRepository<Team> repo = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabaseTeamRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid team repository used.");
            }

            return repo;
        }

        public static IRepository<Player> GetPlayerRepository(RepositoryTypes type)
        {
            IRepository<Player> repo = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabasePlayerRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid player repository type.");
            }

            return repo;
        }

        public static IRepository<Caster> GetCasterRepository(RepositoryTypes type)
        {
            IRepository<Caster> repo = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabaseCasterRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid caster repository type.");
            }

            return repo;
        }

        public static IRepository<HelpfulPeople> GetHelpfulPersonRepository(RepositoryTypes type)
        {
            IRepository<HelpfulPeople> repo = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabaseHelpfulPersonRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid helpful person repository type.");
            }

            return repo;
        }

        public static IRepository<Map> GetMapRepository(RepositoryTypes type)
        {
            IRepository<Map> repo = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabaseMapRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid map repository type.");
            }

            return repo;
        }

        public static IRepository<Weapon> GetWeaponRepository(RepositoryTypes type)
        {
            IRepository<Weapon> repo = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabaseWeaponRepository();
                    break;
                default:
                    throw new ArgumentException("Ivalid weapon repository type.");
            }

            return repo;
        }

        public static IRepository<Sub> GetSubRepository(RepositoryTypes type)
        {
            IRepository<Sub> repo = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabaseSubRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid sub repository type.");
            }

            return repo;
        }

        public static IRepository<Special> GetSpecialRepository(RepositoryTypes type)
        {
            IRepository<Special> repo = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabaseSpecialRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid special repository type.");
            }

            return repo;
        }

        public static IRepository<SystemSwitch> GetSystemSwitchRepository(RepositoryTypes type)
        {
            IRepository<SystemSwitch> repo = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabaseSystemSwitchRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid system switch repository type");
            }

            return repo;
        }

        public static IRepository<GameSetting> GetGameSettingRepository(RepositoryTypes type)
        {
            IRepository<GameSetting> repo = null;

            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabaseGameSettingRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid game setting repository type");
            }

            return repo;
        }

        public static IRepository<Match> GetMatchRepository(RepositoryTypes type)
        {
            IRepository<Match> repo = null;
            switch (type)
            {
                case RepositoryTypes.Database:
                    repo = new DatabaseMatchRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid match repository type");
            }

            return repo;
        }
    }
}
