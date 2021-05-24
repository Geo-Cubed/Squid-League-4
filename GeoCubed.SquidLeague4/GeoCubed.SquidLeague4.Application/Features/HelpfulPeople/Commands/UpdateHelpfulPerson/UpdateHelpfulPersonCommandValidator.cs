using FluentValidation;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.UpdateHelpfulPerson
{
    internal class UpdateHelpfulPersonCommandValidator : AbstractValidator<UpdateHelpfulPersonCommand>
    {
        private IHelpfulPersonRepository _helpfulPersonRepository;

        public UpdateHelpfulPersonCommandValidator(IHelpfulPersonRepository helpfulPersonRepository)
        {
            this._helpfulPersonRepository = helpfulPersonRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(helpfulPersonRepository.GetType(), this.GetType()));

            RuleFor(p => p.Description)
                .MaximumLength(128).WithMessage("The description cannot be over 128 characters");

            RuleFor(p => p.ProfilePictureLink)
                .MaximumLength(2000).WithMessage("The profile picture link cannot be over 2000 characters");

            RuleFor(p => p.TwitterLink)
                .MaximumLength(2000).WithMessage("The twitter link cannot be over 2000 characters");

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("Helpful person has to have a name")
                .MaximumLength(32).WithMessage("The name cannot be over 32 characters");

            RuleFor(p => p.Role)
                .MaximumLength(128).WithMessage("Role cannot be over 128 characters");

            RuleFor(e => e)
                .MustAsync(IsValidPersonId)
                .WithMessage("Helpflul person does not exist");
        }

        private async Task<bool> IsValidPersonId(UpdateHelpfulPersonCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._helpfulPersonRepository.DoesHelpfulPersonExist(e.Id);
        }
    }
}