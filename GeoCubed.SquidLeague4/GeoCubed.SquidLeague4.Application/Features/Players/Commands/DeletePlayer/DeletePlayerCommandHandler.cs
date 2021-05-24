using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.DeletePlayer
{
    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, DeletePlayerCommandReponse>
    {
        private readonly IPlayerRepository _playerRepository;

        public DeletePlayerCommandHandler(IPlayerRepository playerRepository)
        {
            this._playerRepository = playerRepository ?? 
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(playerRepository.GetType(), this.GetType()));
        }

        public async Task<DeletePlayerCommandReponse> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var response = new DeletePlayerCommandReponse();

            var validator = new DeletePlayerCommandValidator(this._playerRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count() > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var errorMsg in validation.Errors)
                {
                    response.ValidationErrors.Add(errorMsg.ErrorMessage);
                }
            }

            if (response.Success)
            {
                response.PlayerId = request.Id;
                var playerToDelete = await this._playerRepository.GetByIdAsync(request.Id);
                response.Success = await this._playerRepository.DeleteAsync(playerToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue deleteing the player";
                }
            }

            return response;
        }
    }
}
