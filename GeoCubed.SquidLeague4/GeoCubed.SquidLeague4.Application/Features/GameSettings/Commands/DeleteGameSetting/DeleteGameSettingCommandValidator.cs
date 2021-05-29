using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.DeleteGameSetting
{
    internal class DeleteGameSettingCommandValidator : AbstractValidator<DeleteGameSettingCommand>
    {
        private IGameSettingRepository _settingRepository;

        public DeleteGameSettingCommandValidator(IGameSettingRepository settingRepository)
        {
            this._settingRepository = settingRepository;

            RuleFor(e => e)
                .MustAsync(DoesGameSettingExist).WithMessage("A game setting with the provided id does not exist");
        }

        private async Task<bool> DoesGameSettingExist(DeleteGameSettingCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._settingRepository.DoesGameSettingExist(e.Id);
        }
    }
}