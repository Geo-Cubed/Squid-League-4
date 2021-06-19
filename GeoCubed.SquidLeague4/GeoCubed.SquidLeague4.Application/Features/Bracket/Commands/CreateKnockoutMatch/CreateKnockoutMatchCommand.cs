using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Commands.CreateKnockoutMatch
{
    public record CreateKnockoutMatchCommand(int MatchId, string Stage) : IRequest<CreateKnockoutMatchCommandResponse>;
}