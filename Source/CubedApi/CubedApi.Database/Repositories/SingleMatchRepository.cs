using CubedApi.CustomExceptions;
using CubedApi.DatabaseInterface;
using CubedApi.Models.DatabaseTables;
using CubedApi.Models.ModelLinkers;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Database.Repositories
{
    public class SingleMatchRepository : IRepository<SingleMatchInformation>
    {
        private IDatabaseConnector connector;

        public SingleMatchRepository(IDatabaseConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException("Repository needs an implementation of a IDatabaseConnector.");
            }

            this.connector = connector;
        }

        public IDatabaseConnector GetConnection()
        {
            return this.connector.GetDBConnection();
        }

        public SingleMatchInformation GetItem(int id)
        {
            if (!this.connector.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to open the database connection");
            }

            var data = new SingleMatchInformation();
            var query = $"call get_single_match_information_by_id(@param_1);";
            var read = this.connector.SelectQuery(query, id);
            while (read.Read())
            {
                data.MatchInforamtion = new Match() 
                {
                    Id = id,
                    HomeTeamId = read.TryGetValue("homeTeamId", out int? homeTeamId) ? homeTeamId : null,
                    AwayTeamId = read.TryGetValue("awayTeamId", out int? awayTeamId) ? awayTeamId : null,
                    HomeTeamName = read.TryGetValue("homeTeamName", out string homeTeamName) ? homeTeamName : string.Empty,
                    AwayTeamName = read.TryGetValue("awayTeamName", out string awayTeamName) ? awayTeamName : string.Empty,
                    HomeTeamScore = read.TryGetValue("homeTeamScore", out int? homeTeamScore) ? homeTeamScore : 0,
                    AwayTeamScore = read.TryGetValue("awayTeamScore", out int? awayTeamScore) ? awayTeamScore : 0,
                    CasterProfileId = read.TryGetValue("casterId", out int? casterId) ? casterId : null,
                    CasterName = read.TryGetValue("casterName", out string casterName) ? casterName : string.Empty,
                    MatchVodLink = read.TryGetValue("vodLink", out string vodLink) ? vodLink : string.Empty,
                    MatchDate = read.TryGetValue("matchDate", out DateTime? matchDate) ? matchDate : null,
                    Week = read.TryGetValue("matchWeek", out int? matchWeek) ? matchWeek : null,
                    Stage = read.TryGetValue("matchStage", out string matchStage) ? matchStage : string.Empty,
                    IsSwiss = !matchWeek.IsNull()
                };
            }

            read.Close();
            query = $"call get_set_information_by_match_id(@param_1);";
            read = this.connector.SelectQuery(query, id);
            while (read.Read())
            {

            }

            this.connector.TryCloseConnection();
            return data;
        }

        public IEnumerable<SingleMatchInformation> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
