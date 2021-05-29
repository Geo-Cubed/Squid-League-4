using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.CreateGameSetting
{
    public class CreateGameSettingCommand : IRequest<CreateGameSettingCommandResponse>
    {
        public int GameMapId { get; set; }

        public int GameModeId { get; set; }

        public string BracketStage { get; set; }

        public int SortOrder { get; set; }
    }
}