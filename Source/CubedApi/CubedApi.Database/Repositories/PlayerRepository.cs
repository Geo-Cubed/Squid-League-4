using CubedApi.CustomExceptions;
using CubedApi.DatabaseInterface;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System.Collections.Generic;

namespace CubedApi.Database.Repositories
{
    public class PlayerRepository : DatabaseConnector, IRepository<Player>
    {
        public PlayerRepository(string connectionStr) : base(connectionStr)
        {
        }

        public IDatabaseConnector GetConnection()
        {
            return this.GetDBConnection();
        }

        public Player GetItem(int id)
        {
            var query = $"call get_player_by_id(@param_1);";
            Player result = null;
            if (!this.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = this.SelectQuery(query, id);
            while (read.Read())
            {
                result = new Player()
                {
                    Id = read.TryGetValue("id", out int? playerId) ? playerId : null,
                    InGameName = read.TryGetValue("playerName", out string playerName) ? playerName : null,
                    TeamName = read.TryGetValue("team", out string teamName) ? teamName : null,
                    SzRank = read.TryGetValue("splatZones", out string szRank) ? szRank : null,
                    RmRank = read.TryGetValue("rainMaker", out string rmRank) ? rmRank : null,
                    CbRank = read.TryGetValue("clamBlitz", out string cbRank) ? cbRank : null,
                    TcRank = read.TryGetValue("towerControl", out string tcRank) ? tcRank : null,
                    Role = read.TryGetValue("role", out string role) ? role : null,
                    Weapon1Path = read.TryGetValue("weapon1", out string weaponOne) ? weaponOne : null,
                    Weapon2Path = read.TryGetValue("weapon2", out string weaponTwo) ? weaponTwo : null,
                    Weapon3Path = read.TryGetValue("weapon3", out string weaponThree) ? weaponThree : null,
                    IsActive = true
                };
            }

            if (!this.TryCloseConnection())
            {
                throw new DatabaseCloseConnectionException("There was an issue while trying to close the database connection");
            }

            return result;
        }

        public IEnumerable<Player> GetItems()
        {
            var query = "call get_all_player_information();";
            var result = new List<Player>();
            if (!this.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = this.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new Player()
                {
                    Id = read.TryGetValue("id", out int? playerId) ? playerId : null,
                    InGameName = read.TryGetValue("playerName", out string playerName) ? playerName : null,
                    TeamName = read.TryGetValue("team", out string teamName) ? teamName : null,
                    SzRank = read.TryGetValue("splatZones", out string szRank) ? szRank : null,
                    RmRank = read.TryGetValue("rainMaker", out string rmRank) ? rmRank : null,
                    CbRank = read.TryGetValue("clamBlitz", out string cbRank) ? cbRank : null,
                    TcRank = read.TryGetValue("towerControl", out string tcRank) ? tcRank : null,
                    Role = read.TryGetValue("role", out string role) ? role : null,
                    Weapon1Path = read.TryGetValue("weapon1", out string weaponOne) ? weaponOne : null,
                    Weapon2Path = read.TryGetValue("weapon2", out string weaponTwo) ? weaponTwo : null,
                    Weapon3Path = read.TryGetValue("weapon3", out string weaponThree) ? weaponThree : null,
                    IsActive = true
                });
            }

            if (!this.TryCloseConnection())
            {
                throw new DatabaseCloseConnectionException("There was an issue while trying to close the database connection");
            }

            return result;
        }
    }
}
