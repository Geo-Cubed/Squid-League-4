using GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterById;
using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterList
{
    public class GetCasterListQuery : IRequest<List<CasterVm>>
    {
    }
}