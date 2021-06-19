using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Commands.CreateKnockoutMatch
{
    public class CreateKnockoutMatchCommandResponse : BaseResponse
    {
        public CreateKnockoutMatchCommandResponse() : base()
        {
        }

        public BracketCommandDto KnockoutMatch { get; set; }
    }
}