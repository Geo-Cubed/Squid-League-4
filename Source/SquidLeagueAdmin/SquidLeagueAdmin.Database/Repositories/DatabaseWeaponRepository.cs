using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.Models.Enums;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using SquidLeagueAdmin.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseWeaponRepository : DatabaseConnector, IRepository<Weapon>
    {
        public bool AddItem(Weapon item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Weapon item)
        {
            throw new NotImplementedException();
        }

        public Weapon GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Weapon> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var result = new List<Weapon>();
            var query = DatabaseQueryHelper.FullQuery(QueryType.Get, DatabaseQueryHelper.WeaponTable);
            try
            {
                var read = this.SelectQuery(query);
                while (read.Read())
                {
                    result.Add(new Weapon()
                    {
                        Id = read.TryGetValue("id", out int? id) ? (int)id : -1,
                        Name = read.TryGetValue("name", out string name) ? name : string.Empty,
                        PicturePath = read.TryGetValue("picturePath", out string path) ? path : string.Empty,
                        SubId = read.TryGetValue("subId", out int? subId) ? (int)subId : -1,
                        SpecialId = read.TryGetValue("specialId", out int? specialId) ? (int)specialId : -1,
                        Type = read.TryGetValue("weaponType", out string weaponType) ? weaponType.GetEnumFromDescription<WeaponType>() : WeaponType.Blaster,
                        Role = read.TryGetValue("weaponRole", out string weaponRole) ? weaponRole.GetEnumFromDescription<WeaponRole>() : WeaponRole.Anchor
                    });
                }
            }
            catch
            {
                result.Add(new Weapon() { Id = -1, Name = "SQL ISSUE -> Tell Geo", Role = WeaponRole.Anchor, Type = WeaponType.Charger });
            }
            finally
            {
                this.TryCloseConnection();
            }
            
            return result;
        }

        public void InsertItems(IEnumerable<Weapon> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Weapon item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue trying to open the database connection.");
            }

            var query = DatabaseQueryHelper.FullQuery(QueryType.Update, DatabaseQueryHelper.WeaponTable, 7);
            try
            {
                this.NoReturnQuery(query, item.Id, item.Name, item.PicturePath, item.SubId, item.SpecialId,
                    item.Type.GetDescription(), item.Role.GetDescription());
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
