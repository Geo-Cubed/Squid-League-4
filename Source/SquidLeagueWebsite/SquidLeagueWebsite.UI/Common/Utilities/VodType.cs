using SquidLeagueWebsite.UI.Common.Helpers;

namespace SquidLeagueWebsite.UI.Common.Utilities
{
    public static class VodType
    {
        public static string GetVodLinkType(string link)
        {
            if (link.ToLower().Contains(VodUrlHelper.Twitch))
            {
                return VodUrlHelper.Twitch;
            }
            else if (link.ToLower().Contains(VodUrlHelper.YouTube))
            {
                return VodUrlHelper.YouTube;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
