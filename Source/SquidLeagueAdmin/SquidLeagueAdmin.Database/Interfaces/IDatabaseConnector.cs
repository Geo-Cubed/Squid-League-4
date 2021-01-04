using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SquidLeagueAdmin.Database.Interfaces
{
    public interface IDatabaseConnector
    {
        public IDatabaseConnector GetDBConnection();

        public bool TryOpenConnection();

        public bool TryCloseConnection();

        public IDataReader SelectQuery(string query, params object?[] args);

        public void NoReturnQuery(string query, params object?[] args);
    }
}
