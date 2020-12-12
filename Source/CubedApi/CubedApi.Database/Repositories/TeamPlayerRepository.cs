using CubedApi.CustomExceptions;
using CubedApi.DatabaseInterface;
using CubedApi.Models.DatabaseTables;
using CubedApi.Models.ModelLinkers;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubedApi.Database.Repositories
{
    public class TeamPlayerRepository : DatabaseConnector, IRepository<TeamPlayers>
    {
        public TeamPlayerRepository(string connectionStr) : base(connectionStr)
        {
        }

        public IDatabaseConnector GetConnection()
        {
            return this.GetDBConnection();
        }

        public TeamPlayers GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TeamPlayers> GetItems()
        {
            var query = "call get_all_team_information();";
            var result = new List<TeamPlayers>();
            if (!this.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("An error occured while trying to open the database connection");
            }

            var read = this.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new TeamPlayers()
                {
                    Id = read.TryGetValue("id", out int? id) ? id : null,
                    TeamName = read.TryGetValue("teamName", out string teamName) ? teamName : null,
                    TeamWins = read.TryGetValue("teamWins", out int? teamWins) ? teamWins : 0,
                    TeamLosses = read.TryGetValue("teamLosses", out int? teamLosses) ? teamLosses : 0
                });
            }

            read.Close();
            query = "call get_all_players_on_teams();";
            read = this.SelectQuery(query);
            while (read.Read())
            {
                var id = read.TryGetValue("teamId", out int? teamId) ? teamId : null;
                if (result.Where(t => t.Id == id).Any())
                {
                    result.Where(t => t.Id == id).First().Players.Add(new Player()
                    {
                        Id = read.TryGetValue("id", out int? playerId) ? playerId : null,
                        InGameName = read.TryGetValue("playerName", out string playerName) ? playerName : null,
                        SzRank = read.TryGetValue("splatZones", out string szRank) ? szRank : null,
                        RmRank = read.TryGetValue("rainMaker", out string rmRank) ? rmRank : null,
                        TcRank = read.TryGetValue("towerControl", out string tcRank) ? tcRank : null,
                        CbRank = read.TryGetValue("clamBlitz", out string cbRank) ? cbRank : null,
                        Role = read.TryGetValue("role", out string role) ? role : null,
                        Weapon1Path = read.TryGetValue("weapon1", out string weapon1) ? weapon1 : null,
                        Weapon2Path = read.TryGetValue("weapon2", out string weapon2) ? weapon2 : null,
                        Weapon3Path = read.TryGetValue("weapon3", out string weapon3) ? weapon3 : null
                    });
                }
            }

            if (!this.TryCloseConnection())
            {
                throw new DatabaseCloseConnectionException("An error occured while trying to close the database connection");
            }

            return result;
        }
    }
}
