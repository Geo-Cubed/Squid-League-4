using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SquidLeagueAdmin.Utilities
{
    public static class Utilities
    {
        public static bool IsNumeric(this string input)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(input);
        }
    }
}
