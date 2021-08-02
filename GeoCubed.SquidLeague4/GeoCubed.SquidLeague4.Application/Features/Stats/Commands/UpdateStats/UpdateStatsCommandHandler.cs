using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task<UpdateStatsCommandResponse> Handle(UpdateStatsCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateStatsCommandResponse();

            var validator = new UpdateStatsCommandValidator(this._statsRepository);

            return response;
        }
    }
}
