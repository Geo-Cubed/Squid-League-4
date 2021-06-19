using GeoCubed.SquidLeague4.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface ISystemSwitchRepository : IAsyncRepository<SystemSwitch>
    {
        Task<bool> DoesSwitchExist(int id);

        Task<bool> DoesKnockoutStageExist(string stage);

        Task<IReadOnlyList<int>> GetSwissWeeks();

        Task<IReadOnlyList<string>> GetUpperStages();

        Task<IReadOnlyList<string>> GetLowerStages();
    }
}
