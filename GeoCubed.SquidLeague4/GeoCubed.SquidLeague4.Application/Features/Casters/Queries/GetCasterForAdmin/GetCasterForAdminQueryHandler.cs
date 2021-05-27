using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterForAdmin
{
    public class GetCasterForAdminQueryHandler : IRequestHandler<GetCasterForAdminQuery, List<CasterAdminVm>>
    {
        private readonly IAsyncRepository<CasterProfile> _casterRepository;
        private readonly IMapper _mapper;

        public GetCasterForAdminQueryHandler(IMapper mapper, IAsyncRepository<CasterProfile> casterRepository)
        {
            this._mapper = mapper;
            this._casterRepository = casterRepository;
        }

        public async Task<List<CasterAdminVm>> Handle(GetCasterForAdminQuery request, CancellationToken cancellationToken)
        {
            var casters = await this._casterRepository.GetAllAsync();
            var mappedCasters = this._mapper.Map<List<CasterAdminVm>>(casters);
            return mappedCasters;
        }
    }
}
