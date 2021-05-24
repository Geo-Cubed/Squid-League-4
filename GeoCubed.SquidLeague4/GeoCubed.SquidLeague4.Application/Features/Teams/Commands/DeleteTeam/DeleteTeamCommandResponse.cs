using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommandResponse : BaseResponse
    {
        public DeleteTeamCommandResponse() : base()
        {
        }

        public int? TeamId { get; set; }
    }
}