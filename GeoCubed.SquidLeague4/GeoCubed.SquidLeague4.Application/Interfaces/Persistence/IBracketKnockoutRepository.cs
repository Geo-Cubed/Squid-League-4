using GeoCubed.SquidLeague4.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IBracketKnockoutRepository : IAsyncRepository<BracketKnockout>
    {
        Task<IReadOnlyList<BracketKnockout>> GetUpperBracket();

        Task<IReadOnlyList<BracketKnockout>> GetLowerBracket();
    }
}
