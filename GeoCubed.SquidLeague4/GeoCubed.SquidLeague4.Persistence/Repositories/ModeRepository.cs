using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class ModeRepository : BaseRepository<GameMode>, IModeRepository
    {
        public ModeRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesModeExist(int id)
        {
            var mode = this._dbContext.GameModes.FirstOrDefault(m => m.Id == id);
            return Task.FromResult(mode != null);
        }
    }
}
