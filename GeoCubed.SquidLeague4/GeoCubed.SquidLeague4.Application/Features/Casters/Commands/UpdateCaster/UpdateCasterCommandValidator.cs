using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Commands.UpdateCaster
{
    internal class UpdateCasterCommandValidator : AbstractValidator<UpdateCasterCommand>
    {
        private readonly ICasterRepository _casterRepository;

        public UpdateCasterCommandValidator(ICasterRepository casterRepository)
        {
            this._casterRepository = casterRepository;

            RuleFor(c => c.CasterName)
                .NotEmpty().WithMessage("Caster needs a name")
                .MaximumLength(255).WithMessage("Name cannot be over 255 characters long");

            RuleFor(c => c.Discord)
                .MaximumLength(255).WithMessage("Discord tag cannot be over 255 characters long");

            RuleFor(c => c.IsActive)
                .NotNull().WithMessage("Caster must have an active status");

            RuleFor(c => c.ProfilePicturePath)
                .MaximumLength(2000).WithMessage("Picture path must not be over 2000 characters");

            RuleFor(c => c.Twitch)
                .MaximumLength(255).WithMessage("Twitch link cannot be over 255 characters long");

            RuleFor(c => c.Twitter)
                .MaximumLength(255).WithMessage("Twitter link cannot be over 255 characters long");

            RuleFor(c => c.YouTube)
                .MaximumLength(255).WithMessage("Youtube link cannot be over 255 characters long");

            RuleFor(e => e)
                .MustAsync(IsValidCasterId)
                .WithMessage("Caster does not exist");
        }

        private async Task<bool> IsValidCasterId(UpdateCasterCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._casterRepository.DoesCasterExist(e.Id);
        }
    }
}