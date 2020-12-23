using CubedApi.CustomExceptions;
using CubedApi.DatabaseInterface;
using CubedApi.Models.ModelLinkers;
using CubedApi.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Database.Repositories
{
    public class SingleMatchRepository : DatabaseConnector, IRepository<SingleMatchInformation>
    {
        public SingleMatchRepository(string connectionStr) : base(connectionStr)
        {
        }

        public IDatabaseConnector GetConnection()
        {
            return this.GetDBConnection();
        }

        public SingleMatchInformation GetItem(int id)
        {
            if (!this.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to open the database connection");
            }

            SingleMatchInformation data;
            var query = "call get_single_match_information_by_id();";
            var read = this.SelectQuery(query);
            while (read.Read())
            {

            }

            if (!this.TryCloseConnection())
            {
                throw new DatabaseCloseConnectionException("There was an issue while trying to close the database connection");
            }

            return data;
        }

        public IEnumerable<SingleMatchInformation> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
