using Newtonsoft.Json;
using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiTeamRepository : ApiConnector, IRepository<TeamPlayers>
    {
        public ApiTeamRepository() : base("team/profile")
        {
        }

        public TeamPlayers GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TeamPlayers> GetItems()
        {
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                var teams = JsonConvert.DeserializeObject<List<TeamPlayers>>(data);
                return teams;
            }
            catch
            {
                return new List<TeamPlayers>();
            }
        }
    }
}
