using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class Match
    {
        public int Id;

        public int HomeTeamId;

        public int AwayTeamId;

        public int HomeTeamScore;

        public int AwayTeamScore;

        public int CasterId;

        public int SecondaryCasterId;

        public string MatchVod;

        public DateTime? MatchDate;
    }
}
