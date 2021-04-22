using Newtonsoft.Json;
using SquidLeagueWebsite.Models;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiTeamMatchesRepository : ApiConnector, IRepository<List<TeamMatches>>
    {
        private const string apiUrl = "team/results/";
        public ApiTeamMatchesRepository() : base(apiUrl)
        {
        }

        public List<TeamMatches> GetItem(int id)
        {
            this.UpdatePath(apiUrl + id.ToString());
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                return JsonConvert.DeserializeObject<List<TeamMatches>>(data);
            }
            catch
            {
                return new List<TeamMatches>();
            }
            finally
            {
                this.UpdatePath(apiUrl);
            }
        }

        public IEnumerable<List<TeamMatches>> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
