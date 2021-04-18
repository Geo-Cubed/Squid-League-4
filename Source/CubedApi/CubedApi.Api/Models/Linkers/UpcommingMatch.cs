using CubedApi.Api.Models.DTOs;

namespace CubedApi.Api.Models.Linkers
{
    public class UpcommingMatch
    {
        public MatchDto Match { get; set; }

        public TeamDto HomeTeam { get; set; }

        public TeamDto AwayTeam { get; set; }
    }
}
