using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.CreateGameSetting
{
    internal class CreateGameSettingCommandValidator : AbstractValidator<CreateGameSettingCommand>
    {
        private readonly IMapRepository _mapRepository;
        private readonly IModeRepository _modeRepository;

        public CreateGameSettingCommandValidator(IMapRepository mapRepository, IModeRepository modeRepository)
        {
            this._modeRepository = modeRepository;
            this._mapRepository = mapRepository;

            RuleFor(g => g.SortOrder)
                .GreaterThan(0).WithMessage("Sort order must be greater than 0");

            RuleFor(g => g.BracketStage)
                .NotEmpty().WithMessage("Game setting must have a bracket setting");

            RuleFor(e => e)
                .MustAsync(DoesMapExist).WithMessage("A map with the provided id does not exist")
                .MustAsync(DoesModeExist).WithMessage("A mode with the provided id does not exist");
        }

        private async Task<bool> DoesMapExist(CreateGameSettingCommand e, CancellationToken token)
        {
            if (e.GameMapId <= 0)
            {
                return false;
            }

            return await this._mapRepository.DoesMapExist(e.GameMapId);
        }

        private async Task<bool> DoesModeExist(CreateGameSettingCommand e, CancellationToken token)
        {
            if (e.GameModeId <= 0)
            {
                return false;
            }

            return await this._modeRepository.DoesModeExist(e.GameModeId);
        }
    }
}