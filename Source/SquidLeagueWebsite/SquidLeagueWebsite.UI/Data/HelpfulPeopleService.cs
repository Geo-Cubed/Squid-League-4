using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.RepoFactory;
using SquidLeagueWebsite.RepositoryInterface;
using System.Collections.Generic;

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
