using Newtonsoft.Json;
using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiPlayerGameRepository : ApiConnector, IRepository<List<PlayerGame>>
    {
        private const string apiUrl = "player/game/";

        public ApiPlayerGameRepository() : base(apiUrl)
        {
        }

        public List<PlayerGame> GetItem(int id)
        {
            this.UpdatePath(apiUrl + id.ToString());
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                var playerGames = JsonConvert.DeserializeObject<List<PlayerGame>>(data);
                return playerGames;
            }
            catch
            {
                return new List<PlayerGame>();
            }
            finally
            {
                this.UpdatePath(apiUrl);
            }
        }

        public IEnumerable<List<PlayerGame>> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
