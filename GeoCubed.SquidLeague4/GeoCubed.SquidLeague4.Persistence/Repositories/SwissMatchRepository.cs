using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class SwissMatchRepository : BaseRepository<BracketSwiss>, ISwissMatchRepository
    {
        public SwissMatchRepository(SquidLeagueDbContext context) : base (context)
        {
        }

        public Task<IReadOnlyList<BracketSwiss>> GetAllSwissMatchesWithMatches()
        {
            var swissMatches = this._dbContext.BracketSwisses
                .Include(s => s.Match)
                .Include(s => s.Match.HomeTeam)
                .Include(s => s.Match.AwayTeam).ToList();            
            return Task.FromResult((IReadOnlyList<BracketSwiss>)swissMatches);
        }
    }
}
