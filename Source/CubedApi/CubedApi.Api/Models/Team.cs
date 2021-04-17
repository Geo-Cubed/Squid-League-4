using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models
{
    public partial class Team
    {
        public Team()
        {
            MatchAwayTeams = new HashSet<Match>();
            MatchHomeTeams = new HashSet<Match>();
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string TeamName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Match> MatchAwayTeams { get; set; }
        public virtual ICollection<Match> MatchHomeTeams { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
