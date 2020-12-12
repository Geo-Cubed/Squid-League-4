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

        public TeamService()
        {
            repo = RepositoryFactory.GetTeamRepository("API");
        }

        public IEnumerable<TeamPlayers> GetAllTeams()
        {
            return this.repo.GetItems();
        }
    }
}
