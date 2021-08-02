using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Commands.CreateStats
{
    public class CreateStatsCommandHandler : IRequestHandler<CreateStatsCommand, CreateStatsCommandResponse>
    {
        private readonly IStatisticRepository _statsRepository;
        private readonly IMapper _mapper;

        public CreateStatsCommandHandler(IMapper mapper, IStatisticRepository statisticRepository)
        {
            this._mapper = mapper;
            this._statsRepository = statisticRepository;
        }

        public async Task<CreateStatsCommandResponse> Handle(CreateStatsCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateStatsCommandResponse();

            var validator = new CreateStatsCommandValidator(this._statsRepository);
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
                var statToAdd = this._mapper.Map<Statistic>(request);
                var stat = await this._statsRepository.AddAsync(statToAdd);
                if (stat.Id <= 0)
                {
                    response.Success = false;
                    response.Message = "There was an issue adding the stat";
                }
            }

            return response;
        }
    }
}
