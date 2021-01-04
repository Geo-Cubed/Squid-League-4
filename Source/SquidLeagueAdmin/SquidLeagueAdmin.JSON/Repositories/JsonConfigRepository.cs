using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;

namespace SquidLeagueAdmin.JSON.Repositories
{
    public class JsonConfigRepository : JsonReader, IRepository<Config>
    {
        private const string path = "D:/config.json";

        public bool AddItem(Config item)
        {
            this.Write(item, path);
            return true;
        }

        public bool DeleteItem(Config item)
        {
            throw new NotImplementedException();
        }

        public Config GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Config> GetItems()
        {
            var item = this.Read<Config>(path);
            return new List<Config>() { item };
        }

        public void InsertItems(IEnumerable<Config> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Config item)
        {
            throw new NotImplementedException();
        }
    }
}
