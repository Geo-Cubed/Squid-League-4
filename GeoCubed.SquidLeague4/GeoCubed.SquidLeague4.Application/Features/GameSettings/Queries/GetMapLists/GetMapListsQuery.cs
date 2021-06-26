using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapLists
{
    public record GetMapListsQuery() : IRequest<List<MapListVm>>;
}