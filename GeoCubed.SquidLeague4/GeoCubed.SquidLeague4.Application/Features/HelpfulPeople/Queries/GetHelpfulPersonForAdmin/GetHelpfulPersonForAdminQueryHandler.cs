using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonForAdmin
{
    public class GetHelpfulPersonForAdminQueryHandler : IRequestHandler<GetHelpfulPersonForAdminQuery, List<HelpfulPersonAdminVm>>
    {
        private readonly IAsyncRepository<HelpfulPerson> _personRepository;
        private readonly IMapper _mapper;

        public GetHelpfulPersonForAdminQueryHandler(IMapper mapper, IAsyncRepository<HelpfulPerson> asyncRepository)
        {
            this._mapper = mapper;
            this._personRepository = asyncRepository;
        }

        public async Task<List<HelpfulPersonAdminVm>> Handle(GetHelpfulPersonForAdminQuery request, CancellationToken cancellationToken)
        {
            var people = await this._personRepository.GetAllAsync();
            var mappedPeople = this._mapper.Map<List<HelpfulPersonAdminVm>>(people);
            return mappedPeople;
        }
    }
}
