using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Commands.DeleteKnockoutMatch
{
    internal class DeleteKnockoutMatchCommandValidator : AbstractValidator<DeleteKnockoutMatchCommand>
    {
        private IBracketKnockoutRepository _bracketRepository;

        public DeleteKnockoutMatchCommandValidator(IBracketKnockoutRepository bracketRepository)
        {
            this._bracketRepository = bracketRepository;

            RuleFor(e => e)
                .MustAsync(DoesBracketMatchExist).WithMessage("There is no bracket match with this id.");
        }

        private async Task<bool> DoesBracketMatchExist(DeleteKnockoutMatchCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._bracketRepository.DoesBracketMatchExist(e.Id);
        }
    }
}