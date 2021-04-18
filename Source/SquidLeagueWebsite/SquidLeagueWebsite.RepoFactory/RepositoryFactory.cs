using SquidLeagueWebsite.ApiRepository;
using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;

namespace SquidLeagueWebsite.RepoFactory
{
    public static class RepositoryFactory
    {
        public static IRepository<Match> GetHomeRepository(string type)
        {
            IRepository<Match> repo = null;

            switch (type)
            {
                case "API":
                    repo = new ApiHomeRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid home repository type");
            }

            return repo;
        }

        public static IRepository<Player> GetPlayerRepository(string type)
        {
            IRepository<Player> repo = null;

            switch (type)
            {
                case "API":
                    repo = new ApiPlayerRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid player repository type");
            }

            return repo;
        }

        public static IRepository<TeamPlayers> GetTeamRepository(string type)
        {
            IRepository<TeamPlayers> repo = null;

            switch (type)
            {
                case "API":
                    repo = new ApiTeamRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid team repository type");
            }

            return repo;
        }

        public static IRepository<Caster> GetCasterRepository(string type)
        {
            IRepository<Caster> repo = null;

            switch (type)
            {
                case "API":
                    repo = new ApiCasterRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid caster repository type");
            }

            return repo;
        }

        public static IRepository<HelpfulPeople> GetHelpfulPeopleRepository(string type)
        {
            IRepository<HelpfulPeople> repo = null;

            switch (type)
            {
                case "API":
                    repo = new ApiHelpfulPeopleRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid helpful people repository type");
            }

            return repo;
        }

        public static IRepository<Team> GetSingleTeamRepository(string type)
        {
            IRepository<Team> repo = null;

            switch (type)
            {
                case "API":
                    repo = new ApiTeamEntityRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid single team repository type");
            }

            return repo;
        }

        public static IRepository<List<Weapon>> GetPlayerCommonWeaponRepository(string type)
        {
            IRepository<List<Weapon>> repo = null;

            switch (type)
            {
                case "API":
                    repo = new ApiCommonWeaponsRepo();
                    break;
                default:
                    throw new ArgumentException("Invlaid common weapons repository type");
            }

            return repo;
        }
    }
}
