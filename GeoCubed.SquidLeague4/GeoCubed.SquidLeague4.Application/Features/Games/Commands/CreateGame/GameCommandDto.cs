namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.CreateGame
{
    public class GameCommandDto
    {
        public int Id { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public int GameSettingId { get; set; }

        public int MatchId { get; set; }
    }
}