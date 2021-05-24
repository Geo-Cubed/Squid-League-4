using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class HelpfulPersonRepository : BaseRepository<HelpfulPerson>, IHelpfulPersonRepository
    {
        public HelpfulPersonRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesHelpfulPersonExist(int id)
        {
            var person = this._dbContext.HelpfulPeople.Any(t => t.Id == id);
            return Task.FromResult(person);
        }
    }
}
