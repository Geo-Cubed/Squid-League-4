using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchList
{
    public class GetMatchListQuery : IRequest<List<MatchDetailVm>>
    {
    }
}