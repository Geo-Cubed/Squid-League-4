using FluentValidation;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.CreateTeam
{
    internal class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(t => t.TeamName)
                .NotEmpty().WithMessage("Team must have a name")
                .MaximumLength(100).WithMessage("Team name cannot be over 100 characters");

            RuleFor(t => t.IsActive)
                .NotNull().WithMessage("Team needs an active status");
        }
    }
}
