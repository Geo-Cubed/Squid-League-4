using FluentValidation;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.CreateHelpfulPerson
{
    internal class CreateHelpfulPersonCommandValidator : AbstractValidator<CreateHelpfulPersonCommand>
    {
        public CreateHelpfulPersonCommandValidator() 
        {
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
        }
    }
}