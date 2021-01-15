using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using System;
using System.Collections.Generic;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseHelpfulPersonRepository : DatabaseConnector, IRepository<HelpfulPeople>
    {
        public bool AddItem(HelpfulPeople item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var query = "call admin_create_helpful_people(@param_1, @param_2, @param_3, @param_4);";
            try
            {
                this.NoReturnQuery(query, item.UserName, item.Description, item.ProfilePicture, item.Twitter);
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

        public bool DeleteItem(HelpfulPeople item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var query = "call admin_delete_helpful_people(@param_1);";
            try
            {
                this.NoReturnQuery(query, item.Id);
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

        public HelpfulPeople GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HelpfulPeople> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var result = new List<HelpfulPeople>();
            var query = "call admin_get_helpful_people();";
            var read = this.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new HelpfulPeople()
                {
                    Id = read.TryGetValue("id", out int? id) ? (int)id : -1,
                    UserName = read.TryGetValue("userName", out string name) ? name : string.Empty,
                    Description = read.TryGetValue("description", out string description) ? description : string.Empty,
                    ProfilePicture = read.TryGetValue("picturePath", out string profilePicture) ? profilePicture : string.Empty,
                    Twitter = read.TryGetValue("twitter", out string twitter) ? twitter : string.Empty
                });
            }

            this.TryCloseConnection();
            return result;
        }

        public void InsertItems(IEnumerable<HelpfulPeople> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(HelpfulPeople item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var query = "call admin_update_helpful_people(@param_1, @param_2, @param_3, @param_4, @param_5);";
            try
            {
                this.NoReturnQuery(query, item.Id, item.UserName, item.Description, item.ProfilePicture, item.Twitter);
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
