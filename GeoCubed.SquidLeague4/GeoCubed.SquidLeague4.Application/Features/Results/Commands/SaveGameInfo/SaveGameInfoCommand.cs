using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetSetInfo;
using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Results.Commands.SaveGameInfo
{
    public class SaveGameInfoCommand : IRequest<SaveGameInfoCommandResponse>
    {
        public int MatchId { get; set; }

        public int GameSettingId { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public BasicPlayerWeapon HomePlayer1 { get; set; }

        public BasicPlayerWeapon HomePlayer2 { get; set; }
        
        public BasicPlayerWeapon HomePlayer3 { get; set; }
        
        public BasicPlayerWeapon HomePlayer4 { get; set; }
        
        public BasicPlayerWeapon AwayPlayer1 { get; set; }
        
        public BasicPlayerWeapon AwayPlayer2 { get; set; }
        
        public BasicPlayerWeapon AwayPlayer3 { get; set; }
        
        public BasicPlayerWeapon AwayPlayer4 { get; set; }
    }
}