namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapLists
{
    public class MapListVm
    {
        public string BracketStage { get; set; }

        public int SortOrder { get; set; }
        
        public MapListMapVm Map { get; set; }
        
        public MapListModeVm Mode { get; set; }
    }
}