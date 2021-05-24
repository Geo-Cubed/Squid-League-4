using System;

namespace GeoCubed.SquidLeague4.Website.Common.Helpers
{
    public static class TimezoneHelper
    {
        public static DateTime ConvertFromUtcToBst(this DateTime utcDate)
        {
            // I tried to use time zone shenanigans but gave up.
            utcDate.AddHours(1);
            return utcDate;
        }
    }
}
