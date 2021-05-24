using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.CreateHelpfulPerson
{
    public class CreateHelpfulPersonCommandHandler : IRequestHandler<CreateHelpfulPersonCommand, CreateHelpfulPersonCommandResponse>
    {
        private readonly IAsyncRepository<HelpfulPerson> _helpfulPersonRepository;
        private readonly IMapper _mapper;

        public CreateHelpfulPersonCommandHandler(IMapper mapper, IAsyncRepository<HelpfulPerson> helpfulPersonRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._helpfulPersonRepository = helpfulPersonRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(helpfulPersonRepository.GetType(), this.GetType()));
        }

        public async Task<CreateHelpfulPersonCommandResponse> Handle(CreateHelpfulPersonCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateHelpfulPersonCommandResponse();

            var validator = new CreateHelpfulPersonCommandValidator();
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count() > 0)
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
                var personToAdd = this._mapper.Map<HelpfulPerson>(request);
                var person = await this._helpfulPersonRepository.AddAsync(personToAdd);
                response.HelpfulPerson = this._mapper.Map<HelpfulPersonDto>(person);
            }

            return response;
        }
    }
}
