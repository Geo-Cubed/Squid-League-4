using Newtonsoft.Json;
using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiHomeRepository : ApiConnector, IRepository<UpcommingMatch>
    {
        public ApiHomeRepository() : base("match/upcomming")
        {
        }

        public UpcommingMatch GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UpcommingMatch> GetItems()
        {
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                var matches = JsonConvert.DeserializeObject<List<UpcommingMatch>>(data);
                return matches;
            }
            catch
            {
                return new List<UpcommingMatch>();
            }
        }
    }
}
