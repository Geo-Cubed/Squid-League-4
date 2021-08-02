using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Commands.DeleteStats
{
    public record DeleteStatsCommand(int Id) : IRequest<DeleteStatsCommandResponse>; 
}