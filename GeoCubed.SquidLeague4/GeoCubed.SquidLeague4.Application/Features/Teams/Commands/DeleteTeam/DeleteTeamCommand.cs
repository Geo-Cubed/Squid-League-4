using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommand : IRequest<DeleteTeamCommandResponse>
    {
        public int Id { get; set; }
    }
}
