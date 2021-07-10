namespace GeoCubed.SquidLeague4.Website.ViewModels.GameSettings
{
    public class MatchMapListViewModel
    {
        public int GameSettingId { get; set; }

        public int SortOrder { get; set; }

        public MapListMapDto Map { get; set; }

        public MapListModeDto Mode { get; set; }
    }
}
