using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetAllMatchesForAdmin
{
    public class GetAllMatchesForAdminQuery : IRequest<List<MatchAdminVm>>
    {
    }
}