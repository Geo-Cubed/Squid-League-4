using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.RepoFactory;
using SquidLeagueWebsite.RepositoryInterface;
using System.Collections.Generic;

namespace SquidLeagueWebsite.UI.Data
{
    public class HomeService
    {
        private IRepository<Match> repo;

        public HomeService()
        {
            this.repo = RepositoryFactory.GetHomeRepository("API");
        }

        public IEnumerable<Match> GetUpcommingMatches()
        {
            return this.repo.GetItems();
        }
    }
}
