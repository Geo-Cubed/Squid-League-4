using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamWithPlayersList
{
    public record GetTeamWithPlayersQuery (int TeamId) : IRequest<TeamWithPlayersVm>;
}
