using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Commands.DeleteCaster
{
    public class DeleteCasterCommand : IRequest<DeleteCasterCommandResponse>
    {
        public int Id { get; set; }
    }
}