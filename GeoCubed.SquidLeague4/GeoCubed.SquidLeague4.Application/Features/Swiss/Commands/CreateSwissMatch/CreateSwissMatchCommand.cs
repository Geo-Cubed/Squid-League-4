using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Commands.CreateSwissMatch
{
    public record CreateSwissMatchCommand(int MatchId, int MatchWeek) : IRequest<CreateSwissMatchCommandResponse>;
}