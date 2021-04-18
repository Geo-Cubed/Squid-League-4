using Newtonsoft.Json;
using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiHomeRepository : ApiConnector, IRepository<Match>
    {
        public ApiHomeRepository() : base("matches/upcomming")
        {
        }

        public Match GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Match> GetItems()
        {
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                var matches = JsonConvert.DeserializeObject<List<Match>>(data);
                return matches;
            }
            catch
            {
                return new List<Match>();
            }
        }
    }
}
