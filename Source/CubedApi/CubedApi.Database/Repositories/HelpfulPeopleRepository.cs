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
    public class HelpfulPeopleRepository : IRepository<HelpfulPeople>
    {
        private IDatabaseConnector connector;
        public HelpfulPeopleRepository(IDatabaseConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException("Repository needs an implementation of a IDatabaseConnector.");
            }

            this.connector = connector;
        }

        public IDatabaseConnector GetConnection()
        {
            throw new NotImplementedException();
        }

        public HelpfulPeople GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HelpfulPeople> GetItems()
        {
            if (!this.connector.TryOpenConnection())
            {
                throw new DatabaseOpenConnectionException("There was an issue while trying to open the database connection");
            }

            var query = "call get_all_helpful_people_information();";
            var data = new List<HelpfulPeople>();
            var read = this.connector.SelectQuery(query);
            while (read.Read())
            {
                data.Add(new HelpfulPeople() 
                {
                    Id = read.TryGetValue("id", out int? personId) ? personId : null,
                    UserName = read.TryGetValue("user_name", out string userName) ? userName : null,
                    Description = read.TryGetValue("description", out string description) ? description : null,
                    PicturePath = read.TryGetValue("profile_picture_link", out string picturePath) ? picturePath : null,
                    Twitter = read.TryGetValue("twitter_link", out string twitterLink) ? twitterLink : null
                });
            }

            this.connector.TryCloseConnection();
            return data;
        }
    }
}
