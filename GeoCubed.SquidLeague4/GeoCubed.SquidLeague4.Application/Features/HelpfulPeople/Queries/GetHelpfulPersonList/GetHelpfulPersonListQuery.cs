using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonById;
using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonList
{
    public class GetHelpfulPersonListQuery : IRequest<List<HelpfulPersonVm>>
    {
    }
}
