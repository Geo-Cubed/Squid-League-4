using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Queries.GetAllSwitchesForAdmin
{
    public class GetAllSwitchesForAdminQuery : IRequest<List<SystemSwitchAdminVm>>
    {
    }
}