using GeoCubed.SquidLeague4.Domain.Entities;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface ICasterRepository : IAsyncRepository<CasterProfile>
    {
        Task<bool> DoesCasterExist(int id);
    }
}
