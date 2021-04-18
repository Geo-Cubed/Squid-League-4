using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.RepoFactory;
using SquidLeagueWebsite.RepositoryInterface;
using System.Collections.Generic;

namespace SquidLeagueWebsite.UI.Data
{
    public class HomeService
    {
        private IRepository<UpcommingMatch> repo;

        public HomeService()
        {
            this.repo = RepositoryFactory.GetHomeRepository("API");
        }

        public IEnumerable<UpcommingMatch> GetUpcommingMatches()
        {
            return this.repo.GetItems();
        }
    }
}
