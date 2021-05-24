using GeoCubed.SquidLeague4.Application.Features.Games.Commands.CreateGame;
using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.UpdateGame
{
    public class UpdateGameCommandResponse : BaseResponse
    {
        public UpdateGameCommandResponse() : base()
        {
        }

        public GameCommandDto Game { get; set; }
    }
}