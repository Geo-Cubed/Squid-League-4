using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Exceptions;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterById
{
    public class GetCasterByIdQueryHandler : IRequestHandler<GetCasterByIdQuery, CasterVm>
    {
        private readonly IAsyncRepository<CasterProfile> _casterRepository;
        private readonly IMapper _mapper;

        public GetCasterByIdQueryHandler(IMapper mapper, IAsyncRepository<CasterProfile> casterRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._casterRepository = casterRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(casterRepository.GetType(), this.GetType()));
        }

        public async Task<CasterVm> Handle(GetCasterByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new BadRequestException("Invalid request id");
            }

            var caster = await this._casterRepository.GetByIdAsync(request.Id);
            if (caster == null)
            {
                throw new NotFoundException("Caster", request.Id);
            }

            return this._mapper.Map<CasterVm>(caster);
        }
    }
}
