using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.RepoFactory;
using SquidLeagueWebsite.RepositoryInterface;
using System.Collections.Generic;

namespace SquidLeagueWebsite.UI.Data
{
    public class CasterService
    {
        private IRepository<Caster> repo;

        public CasterService()
        {
            this.repo = RepositoryFactory.GetCasterRepository("API");
        }

        public IEnumerable<Caster> GetAllCasters()
        {
            return this.repo.GetItems();
        }
    }
}
