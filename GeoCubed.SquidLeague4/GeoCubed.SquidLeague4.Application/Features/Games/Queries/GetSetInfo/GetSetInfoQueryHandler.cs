using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetSetInfo
{
    public class GetSetInfoQueryHandler : IRequestHandler<GetSetInfoQuery, List<SetInformationVm>>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetSetInfoQueryHandler(IMapper mapper, IGameRepository gameRepository)
        {
            this._gameRepository = gameRepository;
            this._mapper = mapper;
        }

        public Task<List<SetInformationVm>> Handle(GetSetInfoQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Get maps for match id.
            // Get set info.
            // Fill up any blank spaces (count set info vs count maps)
        }
    }
}
