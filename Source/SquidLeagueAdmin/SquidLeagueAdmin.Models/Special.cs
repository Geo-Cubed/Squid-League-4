using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class Special
    {
        public int Id;

        public string Name;

        public override string ToString()
        {
            return this.Name;
        }
    }
}
