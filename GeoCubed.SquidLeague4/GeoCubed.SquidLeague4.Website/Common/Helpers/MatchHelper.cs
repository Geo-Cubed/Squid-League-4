using GeoCubed.SquidLeague4.Website.ViewModels.SwissMatches;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;

namespace GeoCubed.SquidLeague4.Website.Common.Helpers
{
    public static class MatchHelper
    {
        public static bool IsMatchBye(TeamMatchViewModel match)
        {
            if (match != null && match.HomeTeam != "BYE" && match.AwayTeam != "BYE")
            {
                return false;
            }

            return true;
        }

        public static bool IsMatchBye(SwissMatchDetailsViewModel match)
        {
            if (match.Match != null && match.Match.HomeTeam != "BYE" && match.Match.AwayTeam != "BYE")
            {
                return false;
            }

            return true;
        }
    }
}
