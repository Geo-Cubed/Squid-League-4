using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.RepoFactory;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.UI.Data
{
    public class HelpfulPeopleService
    {
        private IRepository<HelpfulPeople> repo;

        public HelpfulPeopleService()
        {
            this.repo = RepositoryFactory.GetHelpfulPeopleRepository("API");
        }

        public IEnumerable<HelpfulPeople> GetAllHelpfulPeople()
        {
            return this.repo.GetItems();
        }
    }
}
