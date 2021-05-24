using GeoCubed.SquidLeague4.Application.Features.Casters.Commands.CreateCaster;
using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Commands.UpdateCaster
{
    public class UpdateCasterCommandResponse : BaseResponse
    {
        public UpdateCasterCommandResponse() : base()
        {
        }

        public CasterCommandDto Caster { get; set; }
    }
}