using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandResponse : BaseResponse
    {
        public CreateTeamCommandResponse()
        {
        }

        public TeamCommandDto Team { get; set; }
    }
}
