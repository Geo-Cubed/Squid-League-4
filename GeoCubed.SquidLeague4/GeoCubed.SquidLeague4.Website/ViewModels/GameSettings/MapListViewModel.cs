namespace GeoCubed.SquidLeague4.Website.ViewModels.GameSettings
{
    public class MapListViewModel
    {
        public string BracketStage { get; set; }

        public int SortOrder { get; set; }

        public MapListMapDto Map { get; set; }

        public MapListModeDto Mode { get; set; }
    }
}
