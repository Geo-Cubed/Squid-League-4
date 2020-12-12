using Newtonsoft.Json;
using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiCasterRepository : ApiConnector, IRepository<Caster>
    {
        public ApiCasterRepository() : base("caster")
        {
        }

        public Caster GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Caster> GetItems()
        {
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                var teams = JsonConvert.DeserializeObject<List<Caster>>(data);
                return teams;
            }
            catch
            {
                return new List<Caster>();
            }
        }
    }
}
