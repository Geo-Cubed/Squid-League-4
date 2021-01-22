using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class SystemSwitch
    {
        public int Id;

        public string Name;

        public string Value;

        public override string ToString()
        {
            return $"{this.Name} {((this.Value == null) ? string.Empty : $" - {this.Value}")}";
        }
    }
}
