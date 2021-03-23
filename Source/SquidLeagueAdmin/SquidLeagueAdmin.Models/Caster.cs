using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class Caster
    {
        public int Id;

        public string Name;

        public string Twitter;

        public string Youtube;

        public string Twitch;

        public string Discord;

        public string ProfilePicture;

        public int IsActive;

        public override string ToString()
        {
            if (Id <= 0 && string.IsNullOrEmpty(Name))
            {
                return "No Caster";
            }

            return $"{this.Name} - {((this.IsActive == 0) ? "Not" : "")} Active";
        }
    }
}
