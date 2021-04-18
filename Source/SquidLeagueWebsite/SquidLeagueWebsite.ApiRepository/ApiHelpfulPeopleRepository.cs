using Newtonsoft.Json;
using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiHelpfulPeopleRepository : ApiConnector, IRepository<HelpfulPeople>
    {
        public ApiHelpfulPeopleRepository() : base("helpfulpeople")
        {
        }

        public HelpfulPeople GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HelpfulPeople> GetItems()
        {
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                var teams = JsonConvert.DeserializeObject<List<HelpfulPeople>>(data);
                return teams;
            }
            catch
            {
                return new List<HelpfulPeople>();
            }
        }
    }
}
