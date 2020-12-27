using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueWebsite.Models
{
    public class UpcommingMatch
    {
        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public DateTime MatchDate { get; set; }

        public string StreamLink { get; set; }
    }
}
