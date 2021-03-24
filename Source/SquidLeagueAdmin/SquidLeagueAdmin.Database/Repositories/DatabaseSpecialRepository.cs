using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using SquidLeagueAdmin.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseSpecialRepository : DatabaseConnector, IRepository<Special>
    {
        public bool AddItem(Special item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Special item)
        {
            throw new NotImplementedException();
        }

        public Special GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Special> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue trying to open the databse connection.");
            }

            var result = new List<Special>();
            var query = DatabaseQueryHelper.FullQuery(QueryType.Get, DatabaseQueryHelper.SpecialTable);
            var read = this.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new Special() 
                {
                    Id = read.TryGetValue("id", out int? id) ? (int)id : -1,
                    Name = read.TryGetValue("name", out string name) ? name : string.Empty
                });
            }

            this.TryCloseConnection();
            return result;
        }

        public void InsertItems(IEnumerable<Special> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Special item)
        {
            throw new NotImplementedException();
        }
    }
}
