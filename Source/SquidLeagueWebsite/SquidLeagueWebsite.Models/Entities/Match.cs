﻿using System;

namespace SquidLeagueWebsite.Models.Entities
{
    public class Match
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeam { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeam { get; set; }

        public string AwayTeamScore { get; set; }

        public int CasterProfileId { get; set; }

        public string CasterName { get; set; }

        public string MatchVodLink { get; set; }

        public string MatchVodType { get; set; }

        public DateTime? MatchDate { get; set; }

        public bool IsSwiss { get; set; }

        public int? Week { get; set; }

        public string Stage { get; set; }
    }
}