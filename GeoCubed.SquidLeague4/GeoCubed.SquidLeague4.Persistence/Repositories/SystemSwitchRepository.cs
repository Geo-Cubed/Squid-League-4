using GeoCubed.SquidLeague4.Domain.Entities;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class SystemSwitchRepository : BaseRepository<SystemSwitch>, ISystemSwitchRepository
    {
        public SystemSwitchRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesSwitchExist(int id)
        {
            var switches = this._dbContext.SystemSwitches.Where(s => s.Id == id);
            return Task.FromResult(switches.Any());
        }
    }
}
