using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Website.ViewModels.Teams
{
    public class TeamPlayerViewModel
    {
        public string InGameName { get; set; }

        public string SzRank { get; set; }

        public string RmRank { get; set; }

        public string TcRank { get; set; }

        public string CbRank { get; set; }

        public string Role { get; set; }

        public ICollection<CommonWeaponViewModel> Weapons { get; set; }
            = new List<CommonWeaponViewModel>();
    }
}
