using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<CreateTeamCommandResponse>
    {
        public string TeamName { get; set; }

        public bool IsActive { get; set; }
    }
}
