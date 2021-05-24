using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandResponse : BaseResponse
    {
        public CreatePlayerCommandResponse() : base()
        {
        }

        public PlayerCommandDto Player { get; set; }
    }
}
