using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseSystemSwitchRepository : DatabaseConnector, IRepository<SystemSwitch>
    {
        public bool AddItem(SystemSwitch item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var query = "call admin_create_system_switch(@param_1, @param_2);";
            try
            {
                this.NoReturnQuery(query, item.Name, item.Value);
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

        public bool DeleteItem(SystemSwitch item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var query = "call admin_delete_system_switch(@param_1);";
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

        public SystemSwitch GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SystemSwitch> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var result = new List<SystemSwitch>();
            var query = "call admin_get_system_switch();";
            try 
            {
                var read = this.SelectQuery(query);
                while (read.Read())
                {
                    result.Add(new SystemSwitch()
                    {
                        Id = read.TryGetValue("id", out int? id) ? (int)id : -1,
                        Name = read.TryGetValue("name", out string name) ? name : string.Empty,
                        Value = read.TryGetValue("value", out string value) ? value : string.Empty
                    });
                }
            }
            catch
            {
                return new List<SystemSwitch>(); 
            }
            finally
            {
                this.TryCloseConnection();
            }

            return result;
        }

        public void InsertItems(IEnumerable<SystemSwitch> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(SystemSwitch item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var query = "call admin_update_system_switch(@param_1, @param_2, @param_3)";
            try
            {
                this.NoReturnQuery(query, item.Id, item.Name, item.Value);
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
