using CubedApi.Api.Models.DTOs;

namespace CubedApi.Api.Models.Linkers
{
    public class PlayerGame
    {
        public GameDto Game { get; set; }

        public GameMapDto Map { get; set; }

        public GameModeDto Mode { get; set; }

        public TeamDto HomeTeam { get; set; }

        public TeamDto AwayTeam { get; set; }

        public WeaponDto Weapon { get; set; }

        public bool IsHomeTeam { get; set; }
    }
}
