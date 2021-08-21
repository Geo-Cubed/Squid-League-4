using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using GeoCubed.SquidLeague4.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class BracketKnockoutRepository : BaseRepository<BracketKnockout>, IBracketKnockoutRepository
    {
        public BracketKnockoutRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesBracketMatchExist(int id)
        {
            var bracketMatches = this._dbContext.BracketKnockouts.AsNoTracking().Where(x => x.Id == id);
            return Task.FromResult(bracketMatches.Any());
        }

        public Task<IReadOnlyList<BracketKnockout>> GetKnockoutInformation(bool isUpper)
        {
            var stages = this.GetKnockoutStages(isUpper).Result;
            var matches = this._dbContext.BracketKnockouts
                .Include(x => x.Match.HomeTeam)
                .Include(x => x.Match.AwayTeam)
                .Where(x => stages.Contains(x.Stage))
                .ToList();

            return Task.FromResult((IReadOnlyList<BracketKnockout>)matches);
        }

        public Task<IReadOnlyList<BracketKnockout>> GetLowerBracket()
        {
            var lowerStages = this.GetKnockoutStages(false).Result;

            var lowerMatches = this._dbContext.BracketKnockouts
                .Where(x => lowerStages.Contains(x.Stage))
                .ToList();

            return Task.FromResult((IReadOnlyList<BracketKnockout>)lowerMatches);
        }

        public Task<IReadOnlyList<BracketKnockout>> GetUpperBracket()
        {
            var upperStages = this.GetKnockoutStages(true).Result;

            var upperMatches = this._dbContext.BracketKnockouts
                .Where(x => upperStages.Contains(x.Stage))
                .ToList();

            return Task.FromResult((IReadOnlyList<BracketKnockout>)upperMatches);
        }

        private Task<IEnumerable<string>> GetKnockoutStages(bool isUpper)
        {
            var switchName = (isUpper) ? SystemSwitchHelper.UpperStage : SystemSwitchHelper.LowerStage;
            var stages = this._dbContext.SystemSwitches
                .ToList()
                .Where(x => x.Name == switchName)
                .Select(x => x.Value);

            return Task.FromResult(stages);
        }
    }
}
