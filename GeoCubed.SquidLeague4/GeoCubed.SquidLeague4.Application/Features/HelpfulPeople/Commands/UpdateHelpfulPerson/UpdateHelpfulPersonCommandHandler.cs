using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.CreateHelpfulPerson;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.UpdateHelpfulPerson
{
    public class UpdateHelpfulPersonCommandHandler : IRequestHandler<UpdateHelpfulPersonCommand, UpdateHelpfulPersonCommandResponse>
    {
        private readonly IHelpfulPersonRepository _helpfulPersonRepository;
        private readonly IMapper _mapper;

        public UpdateHelpfulPersonCommandHandler(IMapper mapper, IHelpfulPersonRepository helpfulPersonRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._helpfulPersonRepository = helpfulPersonRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(helpfulPersonRepository.GetType(), this.GetType()));
        }

        public async Task<UpdateHelpfulPersonCommandResponse> Handle(UpdateHelpfulPersonCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateHelpfulPersonCommandResponse();

            var validator = new UpdateHelpfulPersonCommandValidator(this._helpfulPersonRepository);
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
                var person = this._mapper.Map<HelpfulPerson>(request);
                response.Success = await this._helpfulPersonRepository.UpdateAsync(person);
                if (!response.Success)
                {
                    response.Message = "There was an issue updating the helpful person";
                    response.HelpfulPerson = this._mapper.Map<HelpfulPersonDto>(person);
                }
            }

            return response;
        }
    }
}
