using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Commands.UpdateStats
{
    public class UpdateStatsCommandHandler : IRequestHandler<UpdateStatsCommand, UpdateStatsCommandResponse>
    {
        private readonly IStatisticRepository _statsRepository;
        private readonly IMapper _mapper;

        public UpdateStatsCommandHandler(IMapper mapper, IStatisticRepository statisticRepository)
        {
            this._mapper = mapper;
            this._statsRepository = statisticRepository;
        }

        public async Task<UpdateStatsCommandResponse> Handle(UpdateStatsCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateStatsCommandResponse();

            var validator = new UpdateStatsCommandValidator(this._statsRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success)
            {
                var statToEdit = this._mapper.Map<Statistic>(request);
                response.Success = await this._statsRepository.UpdateAsync(statToEdit);
                if (!response.Success)
                {
                    response.Message = "There was an issue updating the statistic";
                }
            }

            return response;
        }
    }
}
