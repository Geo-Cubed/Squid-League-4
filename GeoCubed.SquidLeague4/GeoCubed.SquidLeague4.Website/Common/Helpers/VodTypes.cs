namespace GeoCubed.SquidLeague4.Website.Common.Helpers
{
    public static class VodTypes
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
