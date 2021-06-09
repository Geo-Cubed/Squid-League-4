using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Commands.CreateSwissMatch
{
    internal class CreateSwissMatchCommandValidator : AbstractValidator<CreateSwissMatchCommand>
    {
        private IMatchRepository _matchRepository;
        private ISystemSwitchRepository _switchRepository;

        public CreateSwissMatchCommandValidator(IMatchRepository matchRepository, ISystemSwitchRepository switchRepository)
        {
            this._matchRepository = matchRepository;
            this._switchRepository = switchRepository;

            RuleFor(e => e)
                .MustAsync(DoesMatchExist).WithMessage("Match does not exist.")
                .MustAsync(IsValidSwissWeek).WithMessage("Swiss week does not exist.");
        }

        private async Task<bool> DoesMatchExist(CreateSwissMatchCommand e, CancellationToken token)
        {
            if (e.MatchId <= 0)
            {
                return false;
            }

            return await this._matchRepository.DoesMatchExist(e.MatchId);
        }
        
        private async Task<bool> IsValidSwissWeek(CreateSwissMatchCommand e, CancellationToken token)
        {
            if (e.MatchWeek <= 0)
            {
                return false;
            }

            var weeks = await this._switchRepository.GetSwissWeeks();
            return weeks.Contains(e.MatchWeek);
        }
    }
}