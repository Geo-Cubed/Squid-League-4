using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Commands.CreateCaster
{
    public class CreateCasterCommandResponse : BaseResponse
    {
        public CreateCasterCommandResponse() : base()
        {
        }

        public CasterCommandDto Caster { get; set; }
    }
}