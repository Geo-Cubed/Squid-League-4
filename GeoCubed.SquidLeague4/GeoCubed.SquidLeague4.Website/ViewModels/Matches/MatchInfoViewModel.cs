using System;

namespace GeoCubed.SquidLeague4.Website.ViewModels.Matches
{
    public class MatchInfoViewModel
    {
        public int MatchId { get; set; }
        
        public DateTime? MatchDate { get; set; }

        public string MatchVodLink { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public string Winner { get; set; }

        public string Caster { get; set; }

        public string CoCaster { get; set; }
    }
}
