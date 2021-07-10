using GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapLists;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapsByMatchId
{
    public class MatchMapListVm
    {
        public int GameSettingId { get; set; }

        public int SortOrder { get; set; }

        public MapListMapVm Map { get; set; }

        public MapListModeVm Mode { get; set; }
    }
}