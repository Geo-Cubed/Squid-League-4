using Newtonsoft.Json;
using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiPlayerRepository : ApiConnector, IRepository<Player>
    {
        public ApiPlayerRepository() : base("player")
        {
        }

        public Player GetItem(int id)
        {
            // Update url for apiconnector
            throw new NotImplementedException();
        }

        public IEnumerable<Player> GetItems()
        {
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                var players = JsonConvert.DeserializeObject<List<Player>>(data);
                return players;
            }
            catch
            {
                return new List<Player>();
            }
        }
    }
}
