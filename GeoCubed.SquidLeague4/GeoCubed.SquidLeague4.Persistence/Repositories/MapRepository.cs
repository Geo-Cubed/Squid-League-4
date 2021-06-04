using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class MapRepository : BaseRepository<GameMap>, IMapRepository
    {
        public MapRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesMapExist(int id)
        {
            var map = this._dbContext.GameMaps.AsNoTracking().FirstOrDefault(m => m.Id == id);
            return Task.FromResult(map != null);
        }
    }
}
