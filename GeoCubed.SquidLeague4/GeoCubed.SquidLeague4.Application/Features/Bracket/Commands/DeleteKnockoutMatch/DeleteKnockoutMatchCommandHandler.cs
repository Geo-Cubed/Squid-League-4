using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Commands.DeleteKnockoutMatch
{
    public class DeleteKnockoutMatchCommandHandler : IRequestHandler<DeleteKnockoutMatchCommand, DeleteKnockoutMatchCommandResponse>
    {
        private readonly IBracketKnockoutRepository _bracketRepository;

        public DeleteKnockoutMatchCommandHandler (IBracketKnockoutRepository bracketKnockoutRepository)
        {
            this._bracketRepository = bracketKnockoutRepository;
        }

        public async Task<DeleteKnockoutMatchCommandResponse> Handle(DeleteKnockoutMatchCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteKnockoutMatchCommandResponse();

            var validator = new DeleteKnockoutMatchCommandValidator(this._bracketRepository);
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
                var matchToDelete = await this._bracketRepository.GetByIdAsync(request.Id);
                response.Success = await this._bracketRepository.DeleteAsync(matchToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue deleting the match.";
                }
            }

            return response;
        }
    }
}
