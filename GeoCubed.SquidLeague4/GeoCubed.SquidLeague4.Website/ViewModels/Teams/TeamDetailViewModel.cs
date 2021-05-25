using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Website.ViewModels.Teams
{
    public class TeamDetailViewModel
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public bool IsActive { get; set; }

        public int Wins { get; set; } = 0;

        public int Losses { get; set; } = 0;

        public ICollection<TeamPlayerViewModel> Players { get; set; }
    }
}