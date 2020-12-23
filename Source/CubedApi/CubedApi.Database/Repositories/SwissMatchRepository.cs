using CubedApi.CustomExceptions;
using CubedApi.DatabaseInterface;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Database.Repositories
{
    public class SwissMatchRepository : DatabaseConnector, IRepository<Match>
    {
        public SwissMatchRepository(string connectionStr) : base(connectionStr)
        {
        }

        public IDatabaseConnector GetConnection()
        {
            return this.GetDBConnection();
        }

        public Match GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Match> GetItems()
        {
            if (!this.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to open the database connection");
            }

            var data = new List<Match>();
            var query = "call get_all_swiss_match_information();";
            var read = this.SelectQuery(query);
            while (read.Read())
            {
                data.Add(new Match()
                {
                    Id = read.TryGetValue("id", out int? id) ? id : null,
                    HomeTeamId = read.TryGetValue("homeTeamId", out int? homeTeamId) ? homeTeamId : null,
                    AwayTeamId = read.TryGetValue("awayTeamId", out int? awayTeamId) ? awayTeamId : null,
                    HomeTeamName = read.TryGetValue("homeTeamName", out string homeTeamName) ? homeTeamName : string.Empty,
                    AwayTeamName = read.TryGetValue("awayTeamName", out string awayTeamName) ? awayTeamName : string.Empty,
                    HomeTeamScore = read.TryGetValue("homeTeamScore", out int? homeTeamScore) ? homeTeamScore : null,
                    AwayTeamScore = read.TryGetValue("awayTeamScore", out int? awayTeamScore) ? awayTeamScore : null,
                    CasterProfileId = read.TryGetValue("casterId", out int? casterId) ? casterId : null,
                    CasterName = read.TryGetValue("casterName", out string CasterName) ? CasterName : string.Empty,
                    MatchVodLink = read.TryGetValue("vodLink", out string vodLink) ? vodLink : string.Empty,
                    MatchDate = read.TryGetValue("matchDate", out DateTime? matchDate) ? matchDate : null,
                    IsSwiss = true,
                    Week = read.TryGetValue("matchWeek", out int? matchWeek) ? matchWeek : null
                });
            }

            if (!this.TryCloseConnection())
            {
                throw new DatabaseCloseConnectionException("There was an issue while trying to close the database connection");
            }

            return data;
            
        }
    }
}
