using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using SquidLeagueAdmin.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseMapRepository : DatabaseConnector, IRepository<Map>
    {
        public bool AddItem(Map item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Map item)
        {
            throw new NotImplementedException();
        }

        public Map GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Map> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue trying to open the database connection.");
            }

            var result = new List<Map>();
            var query = DatabaseQueryHelper.FullQuery(QueryType.Get, DatabaseQueryHelper.MapTable);
            var read = this.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new Map() 
                { 
                    Id = read.TryGetValue("id", out int? id) ? (int)id : -1,
                    Name = read.TryGetValue("mapName", out string name) ? name : string.Empty,
                    PicturePath = read.TryGetValue("picturePath", out string path) ? path : string.Empty
                });
            }

            this.TryCloseConnection();
            return result;
        }

        public void InsertItems(IEnumerable<Map> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Map item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var query = DatabaseQueryHelper.FullQuery(QueryType.Update, DatabaseQueryHelper.MapTable, 3);
            try
            {
                this.NoReturnQuery(query, item.Id, item.Name, item.PicturePath);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                this.TryCloseConnection();
            }
        }
    }
}
