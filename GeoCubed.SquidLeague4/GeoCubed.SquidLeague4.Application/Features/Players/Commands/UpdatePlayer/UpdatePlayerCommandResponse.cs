using GeoCubed.SquidLeague4.Application.Features.Players.Commands.CreatePlayer;
using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommandResponse : BaseResponse
    {
        public UpdatePlayerCommandResponse() : base()
        {
        }

        public PlayerCommandDto Player { get; set; }
    }
}
