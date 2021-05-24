using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonById
{
    public class GetHelpfulPersonByIdQuery : IRequest<HelpfulPersonVm>
    {
        public int Id { get; set; }
    }
}
