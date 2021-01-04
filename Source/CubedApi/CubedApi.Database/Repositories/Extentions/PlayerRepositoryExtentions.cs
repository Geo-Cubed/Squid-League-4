using CubedApi.CustomExceptions;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Database.Repositories.Extentions
{
    public static class PlayerRepositoryExtentions
    {
        public static IEnumerable<Player> GetPlayersByTeamId(this IRepository<Player> playerRepository, int teamId)
        {
            string query = $"call get_players_by_team_id(@param_1);";
            var connection = playerRepository.GetConnection();
            var result = new List<Player>();
            if (!connection.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = connection.SelectQuery(query, teamId);
            while (read.Read())
            {
                result.Add(new Player()
                {
                    Id = read.TryGetValue("id", out int? playerId) ? playerId : null,
                    InGameName = read.TryGetValue("playerName", out string playerName) ? playerName : null,
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

            if (!connection.TryCloseConnection())
            {
                throw new DatabaseCloseConnectionException("There was an issue while trying to close the database connection");
            }

            return result;
        }
    }
}
