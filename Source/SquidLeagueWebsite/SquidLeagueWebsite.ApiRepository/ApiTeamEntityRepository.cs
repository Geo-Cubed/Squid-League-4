using Newtonsoft.Json;
using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiTeamEntityRepository : ApiConnector, IRepository<Team>
    {
        private const string apiPath = "team/";
        public ApiTeamEntityRepository() : base(apiPath)
        {
        }

        public Team GetItem(int id)
        {
            this.UpdatePath(apiPath + id.ToString());
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                var teams = JsonConvert.DeserializeObject<Team>(data);
                return teams;
            }
            catch
            {
                return new Team();
            }
            finally
            {
                this.UpdatePath(apiPath);
            }
        }

        public IEnumerable<Team> GetItems()
        {
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                var teams = JsonConvert.DeserializeObject<List<Team>>(data);
                return teams;
            }
            catch
            {
                return new List<Team>();
            }
        }
    }
}
