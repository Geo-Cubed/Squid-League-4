using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamById;
using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetActiveTeams
{
    public record GetActiveTeamsQuery() : IRequest<List<TeamVm>>;
}