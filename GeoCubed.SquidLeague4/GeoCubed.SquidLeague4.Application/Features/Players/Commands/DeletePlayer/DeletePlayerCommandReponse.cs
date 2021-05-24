using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.DeletePlayer
{
    public class DeletePlayerCommandReponse : BaseResponse
    {
        public DeletePlayerCommandReponse() : base()
        {
        }

        public int? PlayerId { get; set; }
    }
}
