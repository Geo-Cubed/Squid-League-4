using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamWithPlayersList
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string InGameName { get; set; }

        public string SzRank { get; set; }

        public string RmRank { get; set; }

        public string CbRank { get; set; }

        public string TcRank { get; set; }

        public string Role { get; set; }

        public List<CommonWeaponDto> Weapons { get; set; }
    }
}