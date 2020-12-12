namespace CubedApi.Models.DatabaseTables
{
    public class Weapon
    {
        public int? Id { get; set; }

        public string WeaponName { get; set; }

        public string WeaponPath { get; set; }

        public int? SubId { get; set; }

        public int? SpecialId { get; set; }

        public string WeaponType { get; set; }

        public string WeaponRole { get; set; }
    }
}
