using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Commands.DeleteSwitch
{
    internal class DeleteSwitchCommandValidator : AbstractValidator<DeleteSwitchCommand>
    {
        private ISystemSwitchRepository _switchRepository;

        public DeleteSwitchCommandValidator(ISystemSwitchRepository switchRepository)
        {
            this._switchRepository = switchRepository;

            RuleFor(e => e)
                .MustAsync(DoesExist).WithMessage("There is no system switch with the provided id");
        }

        private async Task<bool> DoesExist(DeleteSwitchCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._switchRepository.DoesSwitchExist(e.Id);
        }
    }
}