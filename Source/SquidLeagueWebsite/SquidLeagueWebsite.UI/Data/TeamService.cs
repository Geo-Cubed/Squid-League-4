using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.RepoFactory;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.UI.Data
{
    public class TeamService
    {
        IRepository<TeamPlayers> repo;
        IRepository<List<TeamMatches>> matchesRepo;

        public TeamService()
        {
            repo = RepositoryFactory.GetTeamRepository("API");
            matchesRepo = RepositoryFactory.GetTeamMatchesRepository("API");
        }

        public List<TeamPlayers> GetAllTeams()
        {
            return this.repo.GetItems().ToList();
        }

        public List<TeamMatches> GetTeamMatches(int id)
        {
            return this.matchesRepo.GetItem(id);
        }
    }
}
