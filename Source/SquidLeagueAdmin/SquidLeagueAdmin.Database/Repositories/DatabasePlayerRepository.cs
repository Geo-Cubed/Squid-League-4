using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.Models.Enums;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabasePlayerRepository : DatabaseConnector, IRepository<Player>
    {
        public DatabasePlayerRepository() : base ()
        {
        }

        public bool AddItem(Player item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var query = "call admin_create_player(@param_1, @param_2, @param_3, @param_4, @param_5, @param_6, @param_7);";
            try
            {
                this.NoReturnQuery(
                    query,
                    item.InGameName,
                    item.SplatZonesRank.GetDescription(),
                    item.RainMakerRank.GetDescription(),
                    item.TowerControlRank.GetDescription(),
                    item.ClamBlitzRank.GetDescription(),
                    item.TeamId,
                    item.IsActive);
            }
            catch
            {
                this.TryCloseConnection();
                return false;
            }

            this.TryCloseConnection();
            return true;
        }

        public bool DeleteItem(Player item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to connect to the database");
            }

            int result = 0;
            var query = "call admin_delete_player(@param_1);";
            var read = this.SelectQuery(query, item.Id);
            while (read.Read())
            {
                result = read.TryGetValue("output", out int? output) ? (int)output : 0;
            }

            this.TryCloseConnection();
            return (result == 1) ? true : false;
            
        }

        public Player GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var items = new List<Player>();
            var query = "call admin_get_all_player_information();";
            var read = this.SelectQuery(query);
            while (read.Read())
            {
                items.Add(new Player()
                {
                    Id = read.TryGetValue("id", out int? id) ? id : -1,
                    InGameName = read.TryGetValue("inGameName", out string inGameName) ? inGameName : "No Name",
                    SplatZonesRank = read.TryGetValue("szRank", out string szRank) ? szRank.GetEnumFromDescription<Ranks>() : Ranks.unknown,
                    RainMakerRank = read.TryGetValue("rmRank", out string rmRank) ? rmRank.GetEnumFromDescription<Ranks>() : Ranks.unknown,
                    TowerControlRank = read.TryGetValue("tcRank", out string tcRank) ? tcRank.GetEnumFromDescription<Ranks>() : Ranks.unknown,
                    ClamBlitzRank = read.TryGetValue("cbRank", out string cbRank) ? cbRank.GetEnumFromDescription<Ranks>() : Ranks.unknown,
                    TeamId = read.TryGetValue("teamId", out int? teamId) ? teamId : -1,
                    IsActive = read.TryGetValue("isActive", out int? isActive) ? (int)isActive : 0
                });
            }

            this.TryCloseConnection();
            return items;
        }

        public void InsertItems(IEnumerable<Player> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Player item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            try
            {
                var query = "call admin_update_player(@param_1, @param_2, @param_3, @param_4, @param_5, @param_6, @param_7, @param_8);";
                this.NoReturnQuery(
                    query,
                    item.Id,
                    item.InGameName,
                    item.SplatZonesRank.GetDescription(),
                    item.RainMakerRank.GetDescription(),
                    item.TowerControlRank.GetDescription(),
                    item.ClamBlitzRank.GetDescription(),
                    item.TeamId,
                    item.IsActive);
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
