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
            var query = DatabaseQueryHelper.FullQuery(
                Utilities.Enums.QueryTypes.get,
                DatabaseQueryHelper.TeamTable,
                DatabaseQueryHelper.BySuffixConstructor(DatabaseQueryHelper.PlayerTable, DatabaseQueryHelper.SuffixId),
                1
            );

            var connection = teamRepository.GetConnection();
            var result = new Team();
            if (!connection.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = connection.SelectQuery(query, id);
            while (read.Read())
            {
                result = new Team()
                {
                    Id = read.TryGetValue("id", out int? teamId) ? teamId : null,
                    TeamName =read.TryGetValue("teamName", out string teamName) ? teamName : string.Empty,
                    IsActive = true
                };
            }

            connection.TryCloseConnection();
            return result;
        }
    }
}
