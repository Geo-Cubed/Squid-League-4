using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterForAdmin
{
    public class GetCasterForAdminQuery : IRequest<List<CasterAdminVm>>
    {
    }
}