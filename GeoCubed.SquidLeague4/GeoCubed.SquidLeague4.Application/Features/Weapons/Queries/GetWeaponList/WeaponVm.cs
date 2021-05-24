namespace GeoCubed.SquidLeague4.Application.Features.Weapons.Queries.GetWeaponList
{
    public class WeaponVm
    {
        public int Id { get; set; }

        public string WeaponName { get; set; }

        public string PicturePath { get; set; }

        public string WeaponType { get; set; }

        public string WeaponRole { get; set; }

        public WeaponSubDto WeaponSub { get; set; }

        public WeaponSpecialDto WeaponSpecial { get; set; }
    }
}