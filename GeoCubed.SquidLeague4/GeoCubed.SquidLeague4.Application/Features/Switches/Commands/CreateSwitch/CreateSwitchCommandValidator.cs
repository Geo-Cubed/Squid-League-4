using FluentValidation;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Commands.CreateSwitch
{
    internal class CreateSwitchCommandValidator : AbstractValidator<CreateSwitchCommand>
    {
        public CreateSwitchCommandValidator()
        {
            RuleFor(s => s.Name)
                .Must(e => { return !string.IsNullOrWhiteSpace(e); }).WithMessage("A system switch name cannot be blank")
                .NotEmpty().WithMessage("A system switch must have a name");

            RuleFor(s => s.Value)
                .Must(e => { return !string.IsNullOrWhiteSpace(e); }).WithMessage("A system switch value cannot be blank")
                .NotEmpty().WithMessage("A system switch must have a value");
        }
    }
}