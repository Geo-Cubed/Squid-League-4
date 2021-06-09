using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Commands.DeleteSwissMatch
{
    public class DeleteSwissMatchCommandHandler : IRequestHandler<DeleteSwissMatchCommand, DeleteSwissMatchCommandResponse>
    {
        private readonly ISwissMatchRepository _swissMatchRepository;
        private readonly IMapper _mapper;

        public DeleteSwissMatchCommandHandler(IMapper mapper, ISwissMatchRepository swissMatchRepository)
        {
            this._mapper = mapper;
            this._swissMatchRepository = swissMatchRepository;
        }

        public async Task<DeleteSwissMatchCommandResponse> Handle(DeleteSwissMatchCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteSwissMatchCommandResponse();

            var validator = new DeleteSwissMatchCommandValidation(this._swissMatchRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count > 0)
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
                var swissToDelete = await this._swissMatchRepository.GetByIdAsync(request.Id);
                response.Success = await this._swissMatchRepository.DeleteAsync(swissToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue deleting the swiss match";
                }
            }

            return response;
        }
    }
}
