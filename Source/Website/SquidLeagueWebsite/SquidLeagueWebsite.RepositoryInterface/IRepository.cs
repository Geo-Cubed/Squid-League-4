using System;
using System.Collections.Generic;

namespace SquidLeagueWebsite.RepositoryInterface
{
    public interface IRepository<T>
    {
        public T GetItem(int id);

        public IEnumerable<T> GetItems();
    }
}
