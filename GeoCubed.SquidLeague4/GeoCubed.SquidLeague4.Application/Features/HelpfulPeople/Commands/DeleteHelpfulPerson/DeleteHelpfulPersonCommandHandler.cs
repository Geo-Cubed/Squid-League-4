using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.DeleteHelpfulPerson
{
    public class DeleteHelpfulPersonCommandHandler : IRequestHandler<DeleteHelpfulPersonCommand, DeleteHelpfulPersonCommandResponse>
    {
        private readonly IHelpfulPersonRepository _helpfulPersonRepository;
        private readonly IMapper _mapper;

        public DeleteHelpfulPersonCommandHandler(IMapper mapper, IHelpfulPersonRepository helpfulPersonRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._helpfulPersonRepository = helpfulPersonRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(helpfulPersonRepository.GetType(), this.GetType()));
        }

        public async Task<DeleteHelpfulPersonCommandResponse> Handle(DeleteHelpfulPersonCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteHelpfulPersonCommandResponse();

            var validator = new DeleteHelpfulPersonCommandValidator(this._helpfulPersonRepository);
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
                response.HelpfulPersonId = request.Id;
                var personToDelete = await this._helpfulPersonRepository.GetByIdAsync(request.Id);
                response.Success = await this._helpfulPersonRepository.DeleteAsync(personToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue deleting the helpful person";
                }
            }

            return response;
        }
    }
}
