using System;

namespace SquidLeagueWebsite.Models
{
    public class Player
    {
        public int? Id { get; set; }

        public string InGameName { get; set; }

        public string SzRank { get; set; }

        public string RmRank { get; set; }

        public string TcRank { get; set; }

        public string CbRank { get; set; }

        public int? TeamId { get; set; }

        public string TeamName { get; set; }

        public bool? IsActive { get; set; }

        public string Role { get; set; }

        public string Weapon1Path { get; set; }

        public string Weapon2Path { get; set; }

        public string Weapon3Path { get; set; }
    }
}
