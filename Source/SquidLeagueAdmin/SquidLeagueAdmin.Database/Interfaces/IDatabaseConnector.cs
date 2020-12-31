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

        public IDataReader SelectQuery(string query);

        public void NoReturnQuery(string query);
    }
}
