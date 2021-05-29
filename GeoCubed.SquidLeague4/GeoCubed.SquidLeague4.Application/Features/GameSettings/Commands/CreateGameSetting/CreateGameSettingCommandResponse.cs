using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.CreateGameSetting
{
    public class CreateGameSettingCommandResponse : BaseResponse
    {
        public CreateGameSettingCommandResponse() : base()
        {
        }

        public GameSettingCommandDto GameSetting { get; set; }
    }
}