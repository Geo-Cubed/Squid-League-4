using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class Game
    {
        public Game()
        {
            this.WeaponPlayeds = new HashSet<WeaponPlayed>();
        }

        public int Id { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public int GameSettingId { get; set; }

        public int MatchId { get; set; }

        public virtual GameSetting GameSetting { get; set; }

        public virtual Match Match { get; set; }

        public virtual ICollection<WeaponPlayed> WeaponPlayeds { get; set; }
    }
}
