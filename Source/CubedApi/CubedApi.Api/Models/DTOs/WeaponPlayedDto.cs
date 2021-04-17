namespace CubedApi.Api.Models.DTOs
{
    public class WeaponPlayedDto
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int WeaponId { get; set; }

        public int GameId { get; set; }

        public bool IsHomeTeam { get; set; }
    }
}
