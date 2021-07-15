using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly SquidLeagueDbContext _dbContext;

        public BaseRepository(SquidLeagueDbContext context)
        {
            this._dbContext = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await this._dbContext.Set<T>().AddAsync(entity);
            await this._dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                this._dbContext.Set<T>().Remove(entity);
                await this._dbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await this._dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await this._dbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                this._dbContext.Entry(entity).State = EntityState.Modified;
                await this._dbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                this._dbContext.Entry(entity).State = EntityState.Unchanged;
                return false;
            }
        }
    }
}
