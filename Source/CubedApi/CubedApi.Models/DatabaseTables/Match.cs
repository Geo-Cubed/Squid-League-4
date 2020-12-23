namespace CubedApi.Models.DatabaseTables
{
    using System;

    public class Match
    {
        public int? Id { get; set; }

        public int? HomeTeamId { get; set; }

        public int? AwayTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int? HomeTeamScore { get; set; }

        public int? AwayTeamScore { get; set; }

        public int? CasterProfileId { get; set; }

        public string CasterName { get; set; }

        public string MatchVodLink { get; set; }

        public DateTime? MatchDate { get; set; }
    }
}
