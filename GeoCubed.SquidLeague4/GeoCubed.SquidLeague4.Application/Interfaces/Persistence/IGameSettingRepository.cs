using GeoCubed.SquidLeague4.Domain.Entities;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IGameSettingRepository : IAsyncRepository<GameSetting>
    {
        Task<bool> DoesGameSettingExist(int id);
    }
}
