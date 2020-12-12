using CubedApi.CustomExceptions;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Database.Repositories.Extentions
{
    public static class TeamRepositoryExtention
    {
        public static Team GetTeamByPlayerId(this IRepository<Team> teamRepository, int id)
        {
            string query = $"call get_team_by_player_id({id});";
            var connection = teamRepository.GetConnection();
            var result = new Team();
            if (!connection.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = connection.SelectQuery(query);
            while (read.Read())
            {
                result = new Team()
                {
                    Id = read.TryGetValue("id", out int? teamId) ? teamId : null,
                    TeamName =read.TryGetValue("teamName", out string teamName) ? teamName : string.Empty,
                    IsActive = true
                };
            }

            if (!connection.TryCloseConnection())
            {
                throw new DatabaseCloseConnectionException("There was an issue while trying to close the connection");
            }

            return result;
        }
    }
}
