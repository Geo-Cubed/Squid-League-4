namespace CubedApi.Models.DatabaseTables
{
    public class WeaponPlayed
    {
        public int? Id { get; set; }

        public int? PlayerId { get; set; }

        public int? WeaponId { get; set; }

        public bool IsHomeTeam { get; set; }
    }
}
