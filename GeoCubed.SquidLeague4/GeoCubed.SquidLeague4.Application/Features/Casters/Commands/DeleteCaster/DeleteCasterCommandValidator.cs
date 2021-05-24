using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Commands.DeleteCaster
{
    internal class DeleteCasterCommandValidator : AbstractValidator<DeleteCasterCommand>
    {
        private ICasterRepository _casterRepository;

        public DeleteCasterCommandValidator(ICasterRepository casterRepository)
        {
            this._casterRepository = casterRepository;

            RuleFor(e => e)
                .MustAsync(IsValidCasterId)
                .WithMessage("Caster with that id does not exist");
        }

        private async Task<bool> IsValidCasterId(DeleteCasterCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._casterRepository.DoesCasterExist(e.Id);
        }
    }
}