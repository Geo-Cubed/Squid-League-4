using System.Collections.Generic;
using SquidLeagueWebsite.RepositoryInterface;
using SquidLeagueWebsite.RepoFactory;
using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.Models;

namespace SquidLeagueWebsite.UI.Data
{
    public class PlayerService
    {
        private IRepository<Player> repo;
        private IRepository<Team> teamRepo;
        private IRepository<List<Weapon>> weaponRepo;
        private IRepository<List<PlayerGame>> playerGameRepo;

        public PlayerService()
        {
            this.repo = RepositoryFactory.GetPlayerRepository("API");
            this.teamRepo = RepositoryFactory.GetSingleTeamRepository("API");
            this.weaponRepo = RepositoryFactory.GetPlayerCommonWeaponRepository("API");
            this.playerGameRepo = RepositoryFactory.GetPlayerGameRepository("API");
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return this.repo.GetItems();
        }  
        
        public Team GetPlayerTeam(int id)
        {
            return this.teamRepo.GetItem(id);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return this.teamRepo.GetItems();
        }

        public IEnumerable<Weapon> GetPlayerWeapons(int id)
        {
            return this.weaponRepo.GetItem(id);
        }

        public IEnumerable<PlayerGame> GetPlayerMatches(int id)
        {
            return this.playerGameRepo.GetItem(id);
        }
    }
}
