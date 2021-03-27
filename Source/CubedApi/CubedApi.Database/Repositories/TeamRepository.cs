using CubedApi.CustomExceptions;
using CubedApi.DatabaseInterface;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using CubedApi.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Database.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private IDatabaseConnector connector;

        public TeamRepository(IDatabaseConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException("Repository needs an implementation of a IDatabaseConnector.");
            }

            this.connector = connector;
        }

        public IDatabaseConnector GetConnection()
        {
            return this.connector.GetDBConnection();
        }

        public Team GetItem(int id)
        {
            var query = DatabaseQueryHelper.FullQuery(
                QueryTypes.get,
                DatabaseQueryHelper.TeamTable,
                DatabaseQueryHelper.BySuffixConstructor(DatabaseQueryHelper.TeamTable, DatabaseQueryHelper.SuffixId),
                1
            );

            Team result = null;
            if (!this.connector.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = this.connector.SelectQuery(query, id);
            while (read.Read())
            {
                result = new Team()
                {
                    Id = read.TryGetValue("id", out int? teamId ) ? teamId : null,
                    TeamName = read.TryGetValue("teamName", out string teamName) ? teamName : null,
                    IsActive = true
                };
            }

            this.connector.TryCloseConnection();
            return result;
        }

        public IEnumerable<Team> GetItems()
        {
            var query = DatabaseQueryHelper.FullQuery(
                QueryTypes.get,
                DatabaseQueryHelper.TeamTable,
                string.Empty
            );

            var result = new List<Team>();
            if (!this.connector.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = this.connector.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new Team()
                {
                    Id = read.TryGetValue("id", out int? teamId) ? teamId : null,
                    TeamName = read.TryGetValue("teamName", out string teamName) ? teamName : null,
                    IsActive = true
                });
            }

            this.connector.TryCloseConnection();
            return result;
        }
    }
}
