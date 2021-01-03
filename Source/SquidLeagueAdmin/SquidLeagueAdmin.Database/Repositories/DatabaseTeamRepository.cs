using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseTeamRepository : DatabaseConnector, IRepository<Team>
    {
        public DatabaseTeamRepository() : base()
        {
        }

        public void AddItem(Team item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Team item)
        {
            throw new NotImplementedException();
        }

        public Team GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Team> GetItems()
        {
            throw new NotImplementedException();
        }

        public void InsertItems(IEnumerable<Team> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Team item)
        {
            throw new NotImplementedException();
        }
    }
}
