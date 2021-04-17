using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models
{
    public partial class CasterProfile
    {
        public CasterProfile()
        {
            MatchCasterProfiles = new HashSet<Match>();
            MatchSecondaryCasterProfiles = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string CasterName { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public string Twitch { get; set; }
        public string Discord { get; set; }
        public string ProfilePicturePath { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Match> MatchCasterProfiles { get; set; }
        public virtual ICollection<Match> MatchSecondaryCasterProfiles { get; set; }
    }
}
