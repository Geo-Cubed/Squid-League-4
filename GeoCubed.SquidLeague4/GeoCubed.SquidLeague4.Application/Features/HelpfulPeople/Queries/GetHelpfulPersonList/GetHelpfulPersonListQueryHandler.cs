using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonById;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonList
{
    public class GetHelpfulPersonListQueryHandler : IRequestHandler<GetHelpfulPersonListQuery, List<HelpfulPersonVm>>
    {
        private readonly IAsyncRepository<HelpfulPerson> _helpfulPersonRepository;
        private readonly IMapper _mapper;

        public GetHelpfulPersonListQueryHandler(IMapper mapper, IAsyncRepository<HelpfulPerson> helpfulPersonRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._helpfulPersonRepository = helpfulPersonRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(helpfulPersonRepository.GetType(), this.GetType()));
        }

        public async Task<List<HelpfulPersonVm>> Handle(GetHelpfulPersonListQuery request, CancellationToken cancellationToken)
        {
            var people = await this._helpfulPersonRepository.GetAllAsync();
            return this._mapper.Map<List<HelpfulPersonVm>>(people);
        }
    }
}
