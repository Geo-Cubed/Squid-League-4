using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Commands.CreateSwissMatch
{
    public class CreateSwissMatchCommandResponse : BaseResponse
    {
        public CreateSwissMatchCommandResponse() : base()
        {
        }

        public SwissCommandDto SwissMatch { get; set; }
    }
}