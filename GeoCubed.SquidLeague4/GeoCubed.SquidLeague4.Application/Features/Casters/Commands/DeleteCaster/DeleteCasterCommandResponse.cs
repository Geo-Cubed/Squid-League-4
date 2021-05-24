using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Commands.DeleteCaster
{
    public class DeleteCasterCommandResponse : BaseResponse
    {
        public DeleteCasterCommandResponse() : base()
        {
        }

        public int? CasterId { get; set; }
    }
}