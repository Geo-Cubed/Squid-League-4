namespace CubedApi.Models.DatabaseTables
{
    public class Game
    {
        public int? Id { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public int? GameSettingId { get; set; }

        public int? MatchId { get; set; }
    }
}
