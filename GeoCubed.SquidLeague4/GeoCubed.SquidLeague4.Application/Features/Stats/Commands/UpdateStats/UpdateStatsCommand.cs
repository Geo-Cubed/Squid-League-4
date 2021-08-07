using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Commands.UpdateStats
{
    public class UpdateStatsCommand : IRequest<UpdateStatsCommandResponse>
    {
        public int Id { get; set; }

        public string Alias { get; set; }

        public string Sql { get; set; }

        public string Modifier { get; set; }
    }
}