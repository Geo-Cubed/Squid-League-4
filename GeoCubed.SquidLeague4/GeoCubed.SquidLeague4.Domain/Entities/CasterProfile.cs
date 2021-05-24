using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class CasterProfile
    {
        public CasterProfile()
        {
            this.MatchCasterProfiles = new HashSet<Match>();
            this.MatchSecondaryCasterProfiles = new HashSet<Match>();
        }

        public int Id { get; set; }

        public string CasterName { get; set; }

        public string Twitter { get; set; }

        public string YouTube { get; set; }

        public string Twitch { get; set; }

        public string Discord { get; set; }

        public string ProfilePicturePath { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Match> MatchCasterProfiles { get; set; }

        public virtual ICollection<Match> MatchSecondaryCasterProfiles { get; set; }
    }
}
