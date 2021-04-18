using SquidLeagueWebsite.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueWebsite.Models
{
    public class UpcommingMatch
    {
        public Match Match { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }
    }
}
