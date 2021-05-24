namespace GeoCubed.SquidLeague4.Website.ViewModels.Admin
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public int GameSettingId { get; set; }

        public int MatchId { get; set; }
    }
}
