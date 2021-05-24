namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetAllGames
{
    public class GameVm
    {
        public int Id { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public int GameSettingId { get; set; }

        public int MatchId { get; set; }
    }
}