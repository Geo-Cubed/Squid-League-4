using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Commands.CreateKnockoutMatch
{
    internal class CreateKnockoutMatchCommandValidator : AbstractValidator<CreateKnockoutMatchCommand>
    {
        private IMatchRepository _matchRepository;
        private ISystemSwitchRepository _switchRepository;

        public CreateKnockoutMatchCommandValidator(IMatchRepository matchRepository, ISystemSwitchRepository switchRepository)
        {
            this._matchRepository = matchRepository;
            this._switchRepository = switchRepository;

            RuleFor(e => e)
                .MustAsync(DoesMatchExist).WithMessage("A match does not exist with that Id.")
                .MustAsync(DoesKnockoutStageExist).WithMessage("There is no knockout stage with that stage Id.");
        }

        private async Task<bool> DoesMatchExist(CreateKnockoutMatchCommand e, CancellationToken token)
        {
            if (e.MatchId <= 0)
            {
                return false;
            }

            return await this._matchRepository.DoesMatchExist(e.MatchId);
        }

        private async Task<bool> DoesKnockoutStageExist(CreateKnockoutMatchCommand e, CancellationToken token)
        {
            if (string.IsNullOrEmpty(e.Stage))
            {
                return false;
            }

            return await this._switchRepository.DoesKnockoutStageExist(e.Stage);
        }
    }
}