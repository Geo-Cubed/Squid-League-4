using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
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

        public bool AddItem(Team item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var query = $"call admin_create_team(@param_1, @param_2);";
            try
            {
                this.NoReturnQuery(query, item.TeamName, item.IsActive);
            }
            catch
            {
                return false;
            }

            this.TryCloseConnection();
            return true;
        }

        public bool DeleteItem(Team item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var query = $"call admin_delete_team(@param_1);";
            bool output = false;
            try
            {
                var read = this.SelectQuery(query, item.Id);
                while (read.Read())
                {
                    output = read.TryGetValue("outputCode", out int? outputCode) ? ((outputCode == 1) ? true : false) : false; 
                }
            }
            catch
            {
                this.TryCloseConnection();
                return false;
            }

            this.TryCloseConnection();
            return output;
        }

        public Team GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Team> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var result = new List<Team>();
            var query = "call admin_get_all_team_information();";
            try
            {
                var read = this.SelectQuery(query);
                while (read.Read())
                {
                    result.Add(new Team()
                    {
                        Id = read.TryGetValue("id", out int? id) ? (int)id : -1,
                        TeamName = read.TryGetValue("teamName", out string teamName) ? teamName : string.Empty,
                        IsActive = read.TryGetValue("isActive", out int? isActive) ? (int)isActive : 0
                    });
                }
            }
            catch
            {
                this.TryCloseConnection();
            }

            this.TryCloseConnection();
            return result;
        }

        public void InsertItems(IEnumerable<Team> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Team item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var query = $"call admin_update_team_information(@param_1, @param_2, @param_3);";
            try
            {
                this.NoReturnQuery(query, item.Id, item.TeamName, item.IsActive);
            }
            catch
            {
                this.TryCloseConnection();
                return false;
            }

            this.TryCloseConnection();
            return true;
        }
    }
}
