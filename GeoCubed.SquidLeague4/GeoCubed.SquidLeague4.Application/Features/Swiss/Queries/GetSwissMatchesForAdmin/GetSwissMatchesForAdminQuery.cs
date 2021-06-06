using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesForAdmin
{
    public class GetSwissMatchesForAdminQuery : IRequest<List<BracketSwissAdminVm>>
    {
    }
}