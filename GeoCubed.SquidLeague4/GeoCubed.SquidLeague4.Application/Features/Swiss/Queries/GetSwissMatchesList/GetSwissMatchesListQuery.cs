using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesList
{
    public class GetSwissMatchesListQuery : IRequest<List<SwissMatchDetailVm>>
    {
    }
}