using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapLists
{
    public class GetMapListsQuery : IRequest<List<MapListVm>>
    {
    }
}