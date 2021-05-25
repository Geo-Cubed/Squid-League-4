using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetTeamPlayedMatches
{
    public class GetTeamPlayedMatchesQuery : IRequest<List<TeamPlayedMatchVm>>
    {
        public int TeamId { get; set; }
    }
}