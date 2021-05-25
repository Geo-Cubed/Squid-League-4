using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamWithPlayersList
{
    public class TeamWithPlayersVm
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public List<PlayerDto> Players { get; set; }
    }
}
