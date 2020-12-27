using System;
using System.Collections.Generic;

namespace SquidLeagueAdmin.RepositoryInterface
{
    public interface IRepository<T>
    {
        public T GetItem(int id);

        public IEnumerable<T> GetItems();

        public void InsertItems(IEnumerable<T> items);

        public void AddItem(T item);

        public bool UpdateItem(T item);

        public bool DeleteItem(T item);
    }
}
