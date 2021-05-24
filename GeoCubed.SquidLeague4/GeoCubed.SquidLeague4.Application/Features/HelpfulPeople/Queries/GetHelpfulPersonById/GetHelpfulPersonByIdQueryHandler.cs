using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Exceptions;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonById
{
    public class GetHelpfulPersonByIdQueryHandler : IRequestHandler<GetHelpfulPersonByIdQuery, HelpfulPersonVm>
    {
        private readonly IAsyncRepository<HelpfulPerson> _helpfulPersonRepository;
        private readonly IMapper _mapper;

        public GetHelpfulPersonByIdQueryHandler(IMapper mapper, IAsyncRepository<HelpfulPerson> helpfulPersonRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._helpfulPersonRepository = helpfulPersonRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(helpfulPersonRepository.GetType(), this.GetType()));
        }

        public async Task<HelpfulPersonVm> Handle(GetHelpfulPersonByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new BadRequestException("Invalid request id.");
            }

            var helpfulPerson = await this._helpfulPersonRepository.GetByIdAsync(request.Id);
            if (helpfulPerson == null)
            {
                throw new NotFoundException("Helpful Person", request.Id);
            }

            return this._mapper.Map<HelpfulPersonVm>(helpfulPerson);
        }
    }
}
