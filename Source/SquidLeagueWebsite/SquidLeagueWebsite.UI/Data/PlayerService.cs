using SquidLeagueWebsite.Models;
using System.Collections.Generic;
using SquidLeagueWebsite.RepositoryInterface;
using SquidLeagueWebsite.RepoFactory;

namespace SquidLeagueWebsite.UI.Data
{
    public class PlayerService
    {
        private IRepository<Player> repo;

        public PlayerService()
        {
            this.repo = RepositoryFactory.GetPlayerRepository("API");
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return this.repo.GetItems();
        }    
    }
}
