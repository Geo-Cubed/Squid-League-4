using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Commands.CreateMatch
{
    public class CreateMatchCommandResponse : BaseResponse
    {
        public CreateMatchCommandResponse() : base()
        {
        }

        public MatchCommandDto Match { get; set; }
    }
}