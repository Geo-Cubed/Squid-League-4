using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public bool is_active { get; set; }
    }
}
