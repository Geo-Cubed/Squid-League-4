using GeoCubed.SquidLeague4.Application.Features.Matches.Commands.CreateMatch;
using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Commands.UpdateMatch
{
    public class UpdateMatchCommandResponse : BaseResponse
    {
        public UpdateMatchCommandResponse() : base()
        {
        }

        public MatchCommandDto Match { get; set; }
    }
}