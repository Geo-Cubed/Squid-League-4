namespace CubedApi.DatabaseInterface
{
    using System.Data;

    public interface IDatabaseConnector
    {
        public IDatabaseConnector GetDBConnection();

        public bool TryOpenConnection();

        public bool TryCloseConnection();

        public IDataReader SelectQuery(string query);

        public void NoReturnQuery(string query);
    }
}
