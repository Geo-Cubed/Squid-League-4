using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.DeleteGame
{
    public class DeleteGameCommandResponse : BaseResponse
    {
        public DeleteGameCommandResponse() : base()
        {
        }

        public int? GameId { get; set; }
    }
}