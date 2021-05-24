using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.DeleteGame
{
    public class DeleteGameCommand : IRequest<DeleteGameCommandResponse>
    {
        public int Id { get; set; }
    }
}