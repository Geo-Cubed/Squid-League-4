using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseCasterRepository : DatabaseConnector, IRepository<Caster>
    {
        public bool AddItem(Caster item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Caster item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var query = "call admin_delete_caster(@param_1)";
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

        public Caster GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Caster> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var result = new List<Caster>();
            var query = "call admin_get_caster();";
            var read = this.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new Caster()
                {
                    Id = read.TryGetValue("id", out int? id) ? (int)id : -1,
                    Name = read.TryGetValue("casterName", out string name) ? name : string.Empty,
                    Twitter = read.TryGetValue("twitter", out string twitter) ? twitter : string.Empty,
                    Youtube = read.TryGetValue("youtube", out string youtube) ? youtube : string.Empty,
                    Twitch = read.TryGetValue("twitch", out string twitch) ? twitch : string.Empty,
                    Discord = read.TryGetValue("youtube", out string discord) ? discord : string.Empty,
                    ProfilePicture = read.TryGetValue("picturePath", out string picturePath) ? picturePath : string.Empty,
                    IsActive = read.TryGetValue("isActive", out int? isActive) ? (int)isActive : 0
                });
            }

            this.TryCloseConnection();
            return result;
        }

        public void InsertItems(IEnumerable<Caster> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Caster item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var query = "call admin_update_caster(@param_1, @param_2, @param_3, @param_4, @param_5, @param_6, @param_7, @param_8);";
            try
            {
                this.NoReturnQuery(query, item.Id, item.Name, item.Twitter, item.Youtube, item.Twitch,
                    item.Discord, item.ProfilePicture, item.IsActive);
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
