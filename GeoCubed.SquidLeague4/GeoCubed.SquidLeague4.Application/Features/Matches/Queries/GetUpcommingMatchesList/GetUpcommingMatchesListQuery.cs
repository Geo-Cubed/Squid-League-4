using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetUpcommingMatchesList
{
    public class GetUpcommingMatchesListQuery : IRequest<List<UpcommingMatchDetailVm>>
    {
    }
}