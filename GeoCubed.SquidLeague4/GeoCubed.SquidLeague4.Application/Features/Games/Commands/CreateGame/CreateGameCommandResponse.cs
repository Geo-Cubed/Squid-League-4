using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.CreateGame
{
    public class CreateGameCommandResponse : BaseResponse
    {
        public CreateGameCommandResponse() : base()
        {
        }

        public GameCommandDto Game { get; set; }
    }
}