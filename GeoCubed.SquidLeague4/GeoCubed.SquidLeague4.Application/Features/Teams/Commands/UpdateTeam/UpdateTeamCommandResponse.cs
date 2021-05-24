using GeoCubed.SquidLeague4.Application.Features.Teams.Commands.CreateTeam;
using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommandResponse : BaseResponse
    {
        public UpdateTeamCommandResponse() : base()
        {
        }

        public TeamCommandDto Team { get; set; }
    }
}
