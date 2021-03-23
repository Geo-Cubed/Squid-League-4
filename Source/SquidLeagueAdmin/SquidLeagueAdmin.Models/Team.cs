using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class Team
    {
        public int Id;

        public string TeamName;

        public int IsActive;

        public override string ToString()
        {
            if (string.IsNullOrEmpty(TeamName))
            {
                return "New Team";
            }

            return $"{TeamName} - {((IsActive == 1) ? "" : "not ")} active";
        }
    }
}
