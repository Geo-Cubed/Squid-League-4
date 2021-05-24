using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamWithPlayersList
{
    public class TeamWithPlayersVm
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public List<PlayerDto> Players { get; set; }
    }
}
