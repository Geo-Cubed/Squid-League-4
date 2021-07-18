using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterById;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterList
{
    public class GetCasterListQueryHandler : IRequestHandler<GetCasterListQuery, List<CasterVm>>
    {
        private readonly IAsyncRepository<CasterProfile> _casterRepository;
        private readonly IMapper _mapper;
        public GetCasterListQueryHandler(IMapper mapper, IAsyncRepository<CasterProfile> casterRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._casterRepository = casterRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(casterRepository.GetType(), this.GetType()));
        }

        public async Task<List<CasterVm>> Handle(GetCasterListQuery request, CancellationToken cancellationToken)
        {
            var casters = (await this._casterRepository.GetAllAsync()).Where(x => x.IsActive);
            return this._mapper.Map<List<CasterVm>>(casters);
        }
    }
}
