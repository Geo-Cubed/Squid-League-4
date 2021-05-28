using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonForAdmin
{
    public class GetHelpfulPersonForAdminQuery : IRequest<List<HelpfulPersonAdminVm>>
    {
    }
}