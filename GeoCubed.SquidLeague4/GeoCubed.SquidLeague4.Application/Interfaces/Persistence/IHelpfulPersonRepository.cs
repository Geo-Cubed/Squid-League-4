using GeoCubed.SquidLeague4.Domain.Entities;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IHelpfulPersonRepository : IAsyncRepository<HelpfulPerson>
    {
        Task<bool> DoesHelpfulPersonExist(int id);
    }
}
