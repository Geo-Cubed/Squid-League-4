using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;

namespace SquidLeagueAdmin.JSON.Repositories
{
    public class JsonConfigRepository : JsonReader, IRepository<Config>
    {
        public bool AddItem(Config item)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
