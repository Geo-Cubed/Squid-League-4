using SquidLeagueWebsite.ApiRepository;
using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.RepositoryInterface;
using System;

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
    }
}
