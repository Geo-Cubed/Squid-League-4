using CubedApi.DatabaseInterface;
using CubedApi.Models.DatabaseTables;
using CubedApi.RepositoryInterface;
using CubedApi.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using CubedApi.Utilities;

namespace CubedApi.Database.Repositories
{
    public class CasterRepository : IRepository<CasterProfile>
    {
        private IDatabaseConnector connector;

        public CasterRepository(IDatabaseConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException("A repository needs an implementation of a database connector.");
            }

            this.connector = connector;
        }

        public IDatabaseConnector GetConnection()
        {
            return this.connector.GetDBConnection();
        }

        public CasterProfile GetItem(int id)
        {
            var query = $"call get_caster_by_id(@param_1);";
            CasterProfile result = null;
            if (!this.connector.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = this.connector.SelectQuery(query, id);
            while (read.Read())
            {
                result = new CasterProfile()
                {
                    Id = id,
                    CasterName = read.TryGetValue("name", out string name) ? name : null,
                    Twitter = read.TryGetValue("twitter", out string twitter) ? twitter : null,
                    YouTube = read.TryGetValue("youtube", out string youtube) ? youtube : null,
                    Twitch = read.TryGetValue("twitch", out string twitch) ? twitch : null,
                    Discord = read.TryGetValue("discord", out string discord) ? discord : null,
                    ProfilePicturePath = read.TryGetValue("picturePath", out string picturePath) ? picturePath : null,
                    IsActive = true
                };
            }

            this.connector.TryCloseConnection();
            return result;
        }

        public IEnumerable<CasterProfile> GetItems()
        {
            var query = "call get_all_caster_information();";
            var result = new List<CasterProfile>();
            if (!this.connector.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to connect to the database");
            }

            var read = this.connector.SelectQuery(query);
            while (read.Read())
            {
                result.Add(new CasterProfile()
                {
                    Id = read.TryGetValue("id", out int? casterId) ? casterId : null,
                    CasterName = read.TryGetValue("name", out string name) ? name : null,
                    Twitter = read.TryGetValue("twitter", out string twitter) ? twitter : null,
                    YouTube = read.TryGetValue("youtube", out string youtube) ? youtube : null,
                    Twitch = read.TryGetValue("twitch", out string twitch) ? twitch : null,
                    Discord = read.TryGetValue("discord", out string discord) ? discord : null,
                    ProfilePicturePath = read.TryGetValue("picturePath", out string picturePath) ? picturePath : null,
                    IsActive = true
                });
            }

            this.connector.TryCloseConnection()
            return result;
        }
    }
}
