using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using SquidLeagueAdmin.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Database.Repositories
{
    public class DatabaseMatchRepository : DatabaseConnector, IRepository<Match>
    {
        public bool AddItem(Match item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var query = DatabaseQueryHelper.FullQuery(QueryType.Create, DatabaseQueryHelper.MatchTable, 6);
            try
            {
                this.NoReturnQuery(
                    query, 
                    item.HomeTeamId, 
                    item.AwayTeamId, 
                    item.CasterId, 
                    item.MatchVod, 
                    item.MatchDate, 
                    item.SecondaryCasterId
                );
                
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

        public bool DeleteItem(Match item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue trying to open the database connection."); 
            }

            var query = DatabaseQueryHelper.FullQuery(QueryType.Delete, DatabaseQueryHelper.MatchTable, 1);
            var read = this.SelectQuery(query, item.Id);
            var isSuccess = false;
            while (read.Read())
            {
                isSuccess = read.TryGetValue("output", out int? output) ? ((output == 1) ? true : false) : false;
            }

            this.TryCloseConnection();
            return isSuccess;
        }

        public Match GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Match> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue while trying to open the database connection.");
            }

            var query = DatabaseQueryHelper.FullQuery(QueryType.Get, DatabaseQueryHelper.MatchTable);
            var matches = new List<Match>();
            var read = this.SelectQuery(query);
            while (read.Read())
            {
                matches.Add(new Match()
                {
                    Id = read.TryGetValue("id", out int? id) ? (int)id : -1,
                    HomeTeamId = read.TryGetValue("homeTeamId", out int? homeTeamId) ? (int)homeTeamId : -1,
                    HomeTeamName = read.TryGetValue("homeTeamName", out string homeTeamName) ? homeTeamName : string.Empty,
                    HomeTeamScore = read.TryGetValue("homeTeamScore", out int? homeTeamScore) ? (int)homeTeamScore : 0,
                    AwayTeamId = read.TryGetValue("awayTeamId", out int? awayTeamId) ? (int)awayTeamId : -1,
                    AwayTeamName = read.TryGetValue("awayTeamName", out string awayTeamName) ? awayTeamName : string.Empty,
                    AwayTeamScore = read.TryGetValue("awayTeamScore", out int? awayTeamScore) ? (int)awayTeamScore : 0,
                    CasterId = read.TryGetValue("casterProfileId", out int? casterProfileId) ? (int)casterProfileId : -1,
                    MatchVod = read.TryGetValue("matchVodLink", out string matchVodLink) ? matchVodLink : string.Empty,
                    MatchDate = read.TryGetValue("matchDate", out DateTime? matchDate) ? matchDate : null
                });
            }

            this.TryCloseConnection();
            return matches;
        }

        public void InsertItems(IEnumerable<Match> items)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(Match item)
        {
            if (!this.TryOpenConnection())
            {
                throw new Exception("There was an issue trying to open the database connection.");
            }

            var query = DatabaseQueryHelper.FullQuery(QueryType.Update, DatabaseQueryHelper.MatchTable, 7);
            try
            {
                this.NoReturnQuery(
                    query,
                    item.Id,
                    item.HomeTeamId,
                    item.AwayTeamId,
                    item.CasterId,
                    item.MatchVod,
                    item.MatchDate,
                    item.SecondaryCasterId
                );

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
