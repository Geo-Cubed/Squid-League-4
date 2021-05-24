using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Commands.DeleteMatch
{
    public class DeleteMatchCommandResponse : BaseResponse
    {
        public DeleteMatchCommandResponse() : base()
        {
        }

        public int? MatchId { get; set; }
    }
}