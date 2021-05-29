using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.DeleteGameSetting
{
    public class DeleteGameSettingCommand : IRequest<DeleteGameSettingCommandResponse>
    {
        public int Id { get; set; }
    }
}