using CubedApi.DatabaseInterface;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Database.Repositories
{
    public class MatchRepository : DatabaseConnector, IRepository<Match>
    {
        public MatchRepository(string connectionStr) : base(connectionStr)
        {
        }

        public IDatabaseConnector GetConnection()
        {
            return this.GetDBConnection();
        }

        public Match GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Match> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
