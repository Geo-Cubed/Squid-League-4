namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByTeamId
{
    public class PlayerWeaponDto
    {
        public string UserName { get; set; }

        public string WeaponName { get; set; }

        public string PicturePath { get; set; }

        public bool IsHomeTeam { get; set; }
    }
}