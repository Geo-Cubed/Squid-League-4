namespace CubedApi.Database
{
    using CubedApi.DatabaseInterface;
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;
    using System.Linq;

    public class DatabaseConnector : IDatabaseConnector
    {
        private MySqlConnection connection;

        public DatabaseConnector(string connectionStr)
        {
            if (string.IsNullOrEmpty(connectionStr))
            {
                throw new ArgumentException("Cannot connect with an empty connection string.");
            }

            this.connection = new MySqlConnection(connectionStr);
        }

        /// <summary>
        /// Tries to open a mysql connection.
        /// </summary>
        /// <returns>True if connection was successful, false is not.</returns>
        public bool TryOpenConnection()
        {
            try
            {
                this.connection.Open();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to close the mysql connection.
        /// </summary>
        /// <returns>True if connection was closed, false if it wasn't.</returns>
        public bool TryCloseConnection()
        {
            try
            {
                this.connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        private void LoadParameters(ref MySqlCommand cmd, object?[] args)
        {
            var counter = 1;
            foreach (var item in args)
            {
                cmd.Parameters.Add(new MySqlParameter($"@param_{counter}", item));
                ++counter;
            }
        }

        /// <summary>
        /// Runs a sql query that has a return.
        /// </summary>
        /// <param name="query">The query to run.</param>
        /// <returns>A data reader containing the query result.</returns>
        public IDataReader SelectQuery(string query, params object?[] args)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("Cannot run an empty query.");
            }

            var cmd = new MySqlCommand(query, this.connection);
            if (args != null && args.Count() > 0)
            {
                this.LoadParameters(ref cmd, args);
            }

            return cmd.ExecuteReader();
        }

        /// <summary>
        /// Runs a sql query that doesn't have a return.
        /// </summary>
        /// <param name="query">The query to run.</param>
        public void NoReturnQuery(string query, params object?[] args)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("Cannot run an empty query.");
            }

            var cmd = new MySqlCommand(query, this.connection);
            if (args != null && args.Count() > 0)
            {
                this.LoadParameters(ref cmd, args);
            }

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Returns the database connector.
        /// </summary>
        /// <returns>The current database connector</returns>
        public IDatabaseConnector GetDBConnection()
        {
            return this;
        }
    }
}
