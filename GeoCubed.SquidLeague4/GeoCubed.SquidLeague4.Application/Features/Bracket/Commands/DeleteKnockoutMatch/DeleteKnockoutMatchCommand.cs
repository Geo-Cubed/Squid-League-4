using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Commands.DeleteKnockoutMatch
{
    public record DeleteKnockoutMatchCommand(int Id) : IRequest<DeleteKnockoutMatchCommandResponse>;
}