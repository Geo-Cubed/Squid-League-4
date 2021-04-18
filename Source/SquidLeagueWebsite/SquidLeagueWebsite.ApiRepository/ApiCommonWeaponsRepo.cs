using Newtonsoft.Json;
using SquidLeagueWebsite.Models.Entities;
using SquidLeagueWebsite.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiCommonWeaponsRepo : ApiConnector, IRepository<List<Weapon>>
    {
        private const string apiUrl = "weapon/";
        public ApiCommonWeaponsRepo() : base(apiUrl)
        {
        }

        public List<Weapon> GetItem(int id)
        {
            this.UpdatePath(apiUrl + id.ToString());
            try
            {
                var data = Task.Run(() => this.GetJsonAsync()).Result;
                var weapons = JsonConvert.DeserializeObject<List<Weapon>>(data);
                return weapons;
            }
            catch
            {
                return new List<Weapon>();
            }
            finally
            {
                this.UpdatePath(apiUrl);
            }
        }

        public IEnumerable<List<Weapon>> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
