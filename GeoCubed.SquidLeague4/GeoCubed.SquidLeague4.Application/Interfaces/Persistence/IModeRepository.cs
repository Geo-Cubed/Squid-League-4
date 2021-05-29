using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IModeRepository
    {
        Task<bool> DoesModeExist(int id);
    }
}
