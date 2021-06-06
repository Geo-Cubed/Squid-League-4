using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetBasicMatchInfo
{
    public class GetBasicMatchInfoQuery : IRequest<List<BasicMatchInfoVm>>
    {
    }
}