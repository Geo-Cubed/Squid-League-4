using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Results.Commands.DeleteGameInfo
{
    public record DeleteGameInfoCommand(int GameId) : IRequest<DeleteGameInfoCommandResponse>;
}
