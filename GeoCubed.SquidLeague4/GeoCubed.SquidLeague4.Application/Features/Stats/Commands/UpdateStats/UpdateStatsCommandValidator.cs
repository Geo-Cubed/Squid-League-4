using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Commands.UpdateStats
{
    internal class UpdateStatsCommandValidator
    {
        private IStatisticRepository statsRepository;

        public UpdateStatsCommandValidator(IStatisticRepository statsRepository)
        {
            this.statsRepository = statsRepository;
        }
    }
}