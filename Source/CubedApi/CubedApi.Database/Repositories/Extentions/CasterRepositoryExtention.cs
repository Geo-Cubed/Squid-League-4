using CubedApi.CustomExceptions;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepositoryInterface;
using CubedApi.Utilities;
using System;

namespace CubedApi.Database.Repositories.Extentions
{
    public static class CasterRepositoryExtention
    {
        public static CasterProfile GetCasterByMatchId(this IRepository<CasterProfile> casterRepository, int id)
        {
            var query = $"call get_caster_by_match_id(@param_1);";
            var connection = casterRepository.GetConnection();
            CasterProfile result = null;
            if (!connection.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = connection.SelectQuery(query, id);
            while (read.Read())
            {
                result = new CasterProfile()
                {
                    Id = read.TryGetValue("id", out int? casterId) ? casterId : null,
                    CasterName = read.TryGetValue("name", out string name) ? name : null,
                    Twitter = read.TryGetValue("twitter", out string twitter) ? twitter : null,
                    YouTube = read.TryGetValue("youtube", out string youtube) ? youtube : null,
                    Twitch = read.TryGetValue("twitch", out string twitch) ? twitch : null,
                    Discord = read.TryGetValue("discord", out string discord) ? discord : null,
                    ProfilePicturePath = read.TryGetValue("picturePath", out string picturePath) ? picturePath : null,
                    IsActive = true
                };
            }

            if (!connection.TryCloseConnection())
            {
                throw new DatabaseCloseConnectionException("There was an issue while trying to close the connection");
            }

            return result;

            throw new NotImplementedException();
        }
    }
}
