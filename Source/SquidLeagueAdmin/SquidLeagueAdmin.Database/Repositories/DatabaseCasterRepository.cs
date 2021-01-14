using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseCasterRepository : DatabaseConnector, IRepository<Caster>
    {
        public bool AddItem(Caster item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Caster item)
        {
            throw new NotImplementedException();
        }

        public Caster GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Caster> GetItems()
        {
            throw new NotImplementedException();
        }

        public void InsertItems(IEnumerable<Caster> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Caster item)
        {
            throw new NotImplementedException();
        }
    }
}
