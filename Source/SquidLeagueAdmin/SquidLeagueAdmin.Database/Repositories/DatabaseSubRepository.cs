using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using SquidLeagueAdmin.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseSubRepository : DatabaseConnector, IRepository<Sub>
    {
        public bool AddItem(Sub item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Sub item)
        {
            throw new NotImplementedException();
        }

        public Sub GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sub> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var result = new List<Sub>();
            var query = DatabaseQueryHelper.FullQuery(QueryType.Get, DatabaseQueryHelper.SubTable);
            var read = this.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new Sub() 
                {
                    Id = read.TryGetValue("id", out int? id) ? (int)id : -1,
                    Name = read.TryGetValue("name", out string name) ? name : string.Empty
                });
            }

            this.TryCloseConnection();
            return result;
        }

        public void InsertItems(IEnumerable<Sub> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Sub item)
        {
            throw new NotImplementedException();
        }
    }
}
