namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesList
{
    public class SwissMatchDetailVm
    {
        public int MatchId { get; set; }
        
        public int MatchWeek { get; set; }

        public SwissMatchDto Match { get; set; }
    }
}