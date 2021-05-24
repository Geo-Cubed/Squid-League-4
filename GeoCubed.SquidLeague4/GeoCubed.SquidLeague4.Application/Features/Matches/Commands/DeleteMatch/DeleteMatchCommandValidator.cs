using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Commands.DeleteMatch
{
    internal class DeleteMatchCommandValidator : AbstractValidator<DeleteMatchCommand>
    {
        private IMatchRepository _matchRepository;

        public DeleteMatchCommandValidator(IMatchRepository matchRepository)
        {
            this._matchRepository = matchRepository;

            RuleFor(e => e)
                .MustAsync(IsValidMatch).WithMessage("Match does not exist");
        }

        private async Task<bool> IsValidMatch(DeleteMatchCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._matchRepository.DoesMatchExist(e.Id);
        }
    }
}