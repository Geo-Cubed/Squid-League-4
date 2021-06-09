using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Commands.DeleteSwissMatch
{
    public record DeleteSwissMatchCommand(int Id) : IRequest<DeleteSwissMatchCommandResponse>;
}