using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Commands.UpdateSwitch
{
    internal class UpdateSwitchCommandValidator : AbstractValidator<UpdateSwitchCommand>
    {
        private ISystemSwitchRepository _switchRepository;

        public UpdateSwitchCommandValidator(ISystemSwitchRepository switchRepository)
        {
            this._switchRepository = switchRepository;

            RuleFor(s => s.Name)
                .Must(e => { return !string.IsNullOrWhiteSpace(e); }).WithMessage("A system switch name cannot be blank")
                .NotEmpty().WithMessage("A system switch must have a name");

            RuleFor(s => s.Value)
                .Must(e => { return !string.IsNullOrWhiteSpace(e); }).WithMessage("A system switch value cannot be blank")
                .NotEmpty().WithMessage("A system switch must have a value");

            RuleFor(e => e)
                .MustAsync(DoesExist).WithMessage("No system switch with the provided id");

        }

        private async Task<bool> DoesExist(UpdateSwitchCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._switchRepository.DoesSwitchExist(e.Id);
        }
    }
}