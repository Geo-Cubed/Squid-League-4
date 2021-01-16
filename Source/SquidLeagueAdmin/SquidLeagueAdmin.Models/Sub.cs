using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class Sub
    {
        public int Id;

        public string Name;

        public override string ToString()
        {
            return this.Name;
        }
    }
}
