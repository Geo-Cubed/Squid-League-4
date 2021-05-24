using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamById
{
    public class GetTeamByIdQuery : IRequest<TeamVm>
    {
        public int Id { get; set; }
    }
}
