namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetBasicMatchInfo
{
    public class BasicMatchInfoVm
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeam { get; set; }

        public string Stage { get; set; }
    }
}