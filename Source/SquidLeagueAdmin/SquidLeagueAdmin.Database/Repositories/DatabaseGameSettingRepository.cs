using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.Models.Enums;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseGameSettingRepository : DatabaseConnector, IRepository<GameSetting>
    {
        public bool AddItem(GameSetting item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var query = "call admin_create_game_setting(@param_1, @param_2, @param_3, @param_4);";
            try
            {
                var read = this.SelectQuery(query, item.MapId, item.Mode.GetDescription(), item.BracketStage, item.SortOrder);
                var result = false;
                while (read.Read())
                {
                    result = read.TryGetValue("output", out int? output) ? (output == 1 ? true : false) : false;
                }

                return result;
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

        public bool DeleteItem(GameSetting item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var query = "call admin_delete_game_setting(@param_1);";
            try
            {
                var result = false;
                var read = this.SelectQuery(query, item.Id);
                while (read.Read())
                {
                    result = read.TryGetValue("output", out int? value) ? (value == 1 ? true : false) : false;
                }

                return result;
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

        public GameSetting GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameSetting> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection");
            }

            var result = new List<GameSetting>();
            var query = "call admin_get_game_setting();";
            var read = this.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new GameSetting()
                {
                    Id = read.TryGetValue("id", out int? id) ? (int)id : -1,
                    MapId = read.TryGetValue("mapId", out int? mapId) ? (int)mapId : -1,
                    Mode = read.TryGetValue("modeName", out string modeName) ? modeName.GetEnumFromDescription<GameModes>() : GameModes.Undefined,
                    BracketStage = read.TryGetValue("stage", out string stage) ? stage : string.Empty,
                    SortOrder = read.TryGetValue("sortOrder", out int? sortOrder) ? (int)sortOrder : -1
                });
            }

            this.TryCloseConnection();
            return result;
        }

        public void InsertItems(IEnumerable<GameSetting> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(GameSetting item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to connect to the database");
            }

            var query = "call admin_update_game_setting(@param_1, @param_2, @param_3, @param_4, @param_5);";
            try
            {
                var read = this.SelectQuery(query, item.Id, item.MapId, item.Mode.GetDescription(), item.BracketStage, item.SortOrder);
                var result = false;
                while (read.Read())
                {
                    result = read.TryGetValue("output", out int? output) ? (output == 1 ? true : false) : false;
                }

                return result;
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
