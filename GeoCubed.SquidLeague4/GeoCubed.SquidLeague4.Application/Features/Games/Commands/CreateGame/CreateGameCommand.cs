using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<CreateGameCommandResponse>
    {
        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public int GameSettingId { get; set; }

        public int MatchId { get; set; }
    }
}