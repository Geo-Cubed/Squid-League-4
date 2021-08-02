using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Commands.DeleteStats
{
    public class DeleteStatsCommandHandler : IRequestHandler<DeleteStatsCommand, DeleteStatsCommandResponse>
    {
        private readonly IStatisticRepository _statsRepository;

        public DeleteStatsCommandHandler(IStatisticRepository statsRepository)
        {
            this._statsRepository = statsRepository;
        }

        public async Task<DeleteStatsCommandResponse> Handle(DeleteStatsCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteStatsCommandResponse();

            response.Success = await this._statsRepository.DoesStatExist(request.Id);
            if (response.Success)
            {
                var statToDelete = await this._statsRepository.GetByIdAsync(request.Id);
                response.Success = await this._statsRepository.DeleteAsync(statToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue deleting the stat";
                }
            }
            else
            {
                response.Message = "The stat does not exist";
            }

            return response;
        }
    }
}
