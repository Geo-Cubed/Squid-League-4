using CubedApi.DatabaseInterface;
using System.Collections.Generic;

namespace CubedApi.RepositoryInterface
{
    public interface IRepository<T>
    {
        public IDatabaseConnector GetConnection();

        public T GetItem(int id);

        public IEnumerable<T> GetItems();
    }
}
