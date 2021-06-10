using GeoCubed.SquidLeague4.Domain.Entities;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using GeoCubed.SquidLeague4.Persistence.Common;

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

        public Task<IReadOnlyList<string>> GetLowerStages()
        {
            return Task.FromResult(this.GetSwitchValues(SystemSwitchHelper.LowerStage));
        }

        public Task<IReadOnlyList<int>> GetSwissWeeks()
        {
            var weeks = this.GetSwitchValues(SystemSwitchHelper.SwissWeek);
            return Task.FromResult((IReadOnlyList<int>)weeks.Select(x => int.Parse(x)).ToList());
        }

        public Task<IReadOnlyList<string>> GetUpperStages()
        {
            return Task.FromResult(this.GetSwitchValues(SystemSwitchHelper.UpperStage));
        }

        private IReadOnlyList<string> GetSwitchValues(string name)
        {
            var stages = this._dbContext.SystemSwitches
                .Where(s => s.Name == name)
                .Select(x => x.Value);
            return stages.ToList();
        }
    }
}
