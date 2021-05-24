using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class CasterRepository : BaseRepository<CasterProfile>, ICasterRepository
    {
        public CasterRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesCasterExist(int id)
        {
            var caster = this._dbContext.CasterProfiles.Any(t => t.Id == id);
            return Task.FromResult(caster);
        }
    }
}
