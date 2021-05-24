using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.DeletePlayer
{
    public class DeletePlayerCommand : IRequest<DeletePlayerCommandReponse>
    {
        public int Id { get; set; }
    }
}
