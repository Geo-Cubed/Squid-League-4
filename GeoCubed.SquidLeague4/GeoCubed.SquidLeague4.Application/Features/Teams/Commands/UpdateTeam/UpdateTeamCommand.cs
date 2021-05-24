using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommand : IRequest<UpdateTeamCommandResponse>
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public bool IsActive { get; set; }
    }
}
