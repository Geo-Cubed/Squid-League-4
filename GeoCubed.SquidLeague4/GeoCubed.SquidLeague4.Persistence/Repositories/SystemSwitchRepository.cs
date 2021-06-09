using GeoCubed.SquidLeague4.Domain.Entities;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class SystemSwitchRepository : BaseRepository<SystemSwitch>, ISystemSwitchRepository
    {
        public SystemSwitchRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesSwitchExist(int id)
        {
            var switches = this._dbContext.SystemSwitches.AsNoTracking().Where(s => s.Id == id);
            return Task.FromResult(switches.Any());
        }

        public Task<IReadOnlyList<int>> GetSwissWeeks()
        {
            var settings = this._dbContext.SystemSwitches.Where(x => x.Name == "SWISS_STAGE").Select(x => int.Parse(x.Value));
            return Task.FromResult((IReadOnlyList<int>)settings.ToList());
        }
    }
}
