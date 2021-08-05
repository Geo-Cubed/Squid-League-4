using GeoCubed.SquidLeague4.Domain.Entities;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IModeRepository : IAsyncRepository<GameMode>
    {
        Task<bool> DoesModeExist(int id);
    }
}
