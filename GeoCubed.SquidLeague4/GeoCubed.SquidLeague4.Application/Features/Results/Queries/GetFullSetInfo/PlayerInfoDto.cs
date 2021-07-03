namespace GeoCubed.SquidLeague4.Application.Features.Results.Queries.GetFullSetInfo
{
    public class PlayerInfoDto
    {
        public int PlayerId { get; set; }

        public string UserName { get; set; }

        public WeaponInfoDto Weapon { get; set; }
    }
}