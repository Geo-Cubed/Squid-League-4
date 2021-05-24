using System;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class Match
    {
        public Match()
        {
            this.BracketKnockouts = new HashSet<BracketKnockout>();
            this.BracketSwisses = new HashSet<BracketSwiss>();
            this.Games = new HashSet<Game>();
        }

        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int? HomeTeamScore { get; set; }

        public int? AwayTeamScore { get; set; }

        public int? CasterProfileId { get; set; }

        public string MatchVodLink { get; set; }

        public DateTime? MatchDate { get; set; }

        public int? SecondaryCasterProfileId { get; set; }

        public virtual Team AwayTeam { get; set; }

        public virtual Team HomeTeam { get; set; }

        public virtual CasterProfile CasterProfile { get; set; }

        public virtual CasterProfile SecondaryCasterProfile { get; set; }

        public virtual ICollection<BracketKnockout> BracketKnockouts { get; set; }

        public virtual ICollection<BracketSwiss> BracketSwisses { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
