namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetSetInfo
{
    public class SetInformationVm
    {
        public int GameId { get; set; }

        public int MatchId { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public BasicPlayerWeapon HomePlayer1 { get; set; }
            = new BasicPlayerWeapon();

        public BasicPlayerWeapon HomePlayer2 { get; set; }
            = new BasicPlayerWeapon();

        public BasicPlayerWeapon HomePlayer3 { get; set; }
            = new BasicPlayerWeapon();

        public BasicPlayerWeapon HomePlayer4 { get; set; }
            = new BasicPlayerWeapon();

        public BasicPlayerWeapon AwayPlayer1 { get; set; }
            = new BasicPlayerWeapon();

        public BasicPlayerWeapon AwayPlayer2 { get; set; }
            = new BasicPlayerWeapon();

        public BasicPlayerWeapon AwayPlayer3 { get; set; }
            = new BasicPlayerWeapon();

        public BasicPlayerWeapon AwayPlayer4 { get; set; }
            = new BasicPlayerWeapon();
    }
}