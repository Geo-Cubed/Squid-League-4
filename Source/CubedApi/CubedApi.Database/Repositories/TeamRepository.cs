using CubedApi.CustomExceptions;
using CubedApi.DatabaseInterface;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Database.Repositories
{
    public class TeamRepository : DatabaseConnector, IRepository<Team>
    {
        public TeamRepository(string connectionStr) : base(connectionStr)
        {
        }

        public IDatabaseConnector GetConnection()
        {
            return this.GetDBConnection();
        }

        public Team GetItem(int id)
        {
            string query = $"call get_team_by_id({id});";
            Team result = null;
            if (!this.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = this.SelectQuery(query);
            while (read.Read())
            {
                result = new Team()
                {
                    Id = read.TryGetValue("id", out int? teamId ) ? teamId : null,
                    TeamName = read.TryGetValue("teamName", out string teamName) ? teamName : null,
                    IsActive = true
                };
            }

            if (!this.TryCloseConnection())
            {
                throw new DatabaseCloseConnectionException("There was an issue while trying to close the connection");
            }

            return result;
        }

        public IEnumerable<Team> GetItems()
        {
            string query = $"call get_all_team_information();";
            var result = new List<Team>();
            if (!this.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = this.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new Team()
                {
                    Id = read.TryGetValue("id", out int? teamId) ? teamId : null,
                    TeamName = read.TryGetValue("teamName", out string teamName) ? teamName : null,
                    IsActive = true
                });
            }

            if (!this.TryCloseConnection())
            {
                throw new DatabaseCloseConnectionException("There was an issue while trying to close the connection");
            }

            return result;
        }
    }
}
