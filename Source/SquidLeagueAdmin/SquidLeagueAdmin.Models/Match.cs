using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class Match
    {
        public int Id;

        public int HomeTeamId;

        public string HomeTeamName;

        public int AwayTeamId;

        public string AwayTeamName;

        public int HomeTeamScore;

        public int AwayTeamScore;

        public int CasterId;

        public int SecondaryCasterId;

        public string MatchVod;

        public DateTime? MatchDate;

        public override string ToString()
        {
            if (Id <= 0)
            {
                return "New Match";
            }

            return $"{HomeTeamName} : {HomeTeamScore} - {AwayTeamScore} : {AwayTeamName}";
        }
    }
}
