using FluentValidation;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.DeletePlayer
{
    internal class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
    {
        private readonly IPlayerRepository _playerRepository;

        public DeletePlayerCommandValidator(IPlayerRepository playerRepository)
        {
            this._playerRepository = playerRepository ?? 
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(playerRepository.GetType(), this.GetType()));

            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("The Id must be greater than 0.");

            RuleFor(e => e)
                .MustAsync(DoesPlayerExist)
                .WithMessage("Player does not exist in the database.");
        }

        private async Task<bool> DoesPlayerExist(DeletePlayerCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._playerRepository.DoesPlayerExist(e.Id);
        }
    }
}
