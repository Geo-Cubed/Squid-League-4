using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);
    }
}
