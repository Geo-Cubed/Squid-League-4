using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Commands.DeleteMatch
{
    public class DeleteMatchCommandHandler : IRequestHandler<DeleteMatchCommand, DeleteMatchCommandResponse>
    {
        private readonly IMatchRepository _matchRepository;

        public DeleteMatchCommandHandler(IMatchRepository matchRepository)
        {
            this._matchRepository = matchRepository;
        }

        public async Task<DeleteMatchCommandResponse> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteMatchCommandResponse();

            var validator = new DeleteMatchCommandValidator(this._matchRepository);
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
                var matchToDelete = await this._matchRepository.GetByIdAsync(request.Id);
                response.Success = await this._matchRepository.DeleteAsync(matchToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue while trying to delete the match";
                    response.MatchId = request.Id;
                }
            }

            return response;
        }
    }
}
