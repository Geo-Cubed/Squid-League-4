using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.UpdateGameSetting
{
    internal class UpdateGameSettingCommandValidator : AbstractValidator<UpdateGameSettingCommand>
    {
        private IGameSettingRepository _settingRepository;
        private IModeRepository _modeRepository;
        private IMapRepository _mapRepository;

        public UpdateGameSettingCommandValidator(IGameSettingRepository settingRepository, IModeRepository modeRepository, IMapRepository mapRepository)
        {
            this._settingRepository = settingRepository;
            this._modeRepository = modeRepository;
            this._mapRepository = mapRepository;

            RuleFor(g => g.SortOrder)
                .GreaterThan(0).WithMessage("Sort order must be greater than 0");

            RuleFor(g => g.BracketStage)
                .NotEmpty().WithMessage("Game setting must have a bracket setting");

            RuleFor(e => e)
                .MustAsync(DoesGameSettingExist).WithMessage("A game setting with the provided id does not exist")
                .MustAsync(DoesMapExist).WithMessage("A map with the provided id does not exist")
                .MustAsync(DoesModeExist).WithMessage("A mode with the provided id does not exist");
        }

        private async Task<bool> DoesGameSettingExist(UpdateGameSettingCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._settingRepository.DoesGameSettingExist(e.Id);
        }

        private async Task<bool> DoesMapExist(UpdateGameSettingCommand e, CancellationToken token)
        {
            if (e.GameMapId <= 0)
            {
                return false;
            }

            return await this._mapRepository.DoesMapExist(e.GameMapId);
        }

        private async Task<bool> DoesModeExist(UpdateGameSettingCommand e, CancellationToken token)
        {
            if (e.GameModeId <= 0)
            {
                return false;
            }

            return await this._modeRepository.DoesModeExist(e.GameModeId);
        }
    }
}