using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Maps.Queries.GetAllMaps
{
    public class GetAllMapsQuery : IRequest<List<MapVm>>
    {
    }
}