using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class Player
    {
        public Player()
        {
            this.WeaponPlayeds = new HashSet<WeaponPlayed>();
        }

        public int Id { get; set; }

        public string InGameName { get; set; }

        public string SzRank { get; set; }

        public string RmRank { get; set; }

        public string TcRank { get; set; }

        public string CbRank { get; set; }

        public int? TeamId { get; set; }

        public bool? IsActive { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<WeaponPlayed> WeaponPlayeds { get; set; }
    }
}
