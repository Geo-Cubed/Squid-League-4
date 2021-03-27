using CubedApi.CustomExceptions;
using CubedApi.DatabaseInterface;
using CubedApi.Models.DatabaseTables;
using CubedApi.Models.ModelLinkers;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using CubedApi.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubedApi.Database.Repositories
{
    public class TeamPlayerRepository : IRepository<TeamPlayers>
    {
        private IDatabaseConnector connector;

        public TeamPlayerRepository(IDatabaseConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException("Repository needs an implementaiton of a IDatabaseConnector.");
            }

            this.connector = connector;
        }

        public IDatabaseConnector GetConnection()
        {
            return this.connector.GetDBConnection();
        }

        public TeamPlayers GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TeamPlayers> GetItems()
        {
            var query = DatabaseQueryHelper.FullQuery(
                QueryTypes.get,
                DatabaseQueryHelper.TeamTable,
                string.Empty
            );

            var result = new List<TeamPlayers>();
            if (!this.connector.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("An error occured while trying to open the database connection");
            }

            var read = this.connector.SelectQuery(query);
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
            query = DatabaseQueryHelper.FullQuery(
                QueryTypes.get,
                DatabaseQueryHelper.PlayerTable,
                DatabaseQueryHelper.OnSuffixConstructor(DatabaseQueryHelper.TeamTable)
            );

            read = this.connector.SelectQuery(query);
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

            this.connector.TryCloseConnection();
            return result;
        }
    }
}
