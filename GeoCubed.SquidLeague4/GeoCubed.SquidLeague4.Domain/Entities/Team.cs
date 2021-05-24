using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class Team
    {
        public Team()
        {
            this.MatchAwayTeams = new HashSet<Match>();
            this.MatchHomeTeams = new HashSet<Match>();
            this.Players = new HashSet<Player>();
        }

        public int Id { get; set; }

        public string TeamName { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<Match> MatchAwayTeams { get; set; }

        public virtual ICollection<Match> MatchHomeTeams { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
