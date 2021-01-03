using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public int IsActive { get; set; }
    }
}
