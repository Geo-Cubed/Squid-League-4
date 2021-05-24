using FluentValidation;
using GeoCubed.SquidLeague4.Application.Common.Enums;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.UpdatePlayer
{
    internal class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public UpdatePlayerCommandValidator(IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            this._playerRepository = playerRepository ?? 
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(playerRepository.GetType(), this.GetType()));
            this._teamRepository = teamRepository ?? 
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(teamRepository.GetType(), this.GetType()));

            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("Player Id must be frater than 0");

            RuleFor(p => p.InGameName)
                .NotEmpty().WithMessage("Name cannot be blank.")
                .MinimumLength(1).WithMessage("Name must have a length greater than 0.")
                .MaximumLength(10).WithMessage("Name must not be over 10 characters long.");

            var ranks = Enum.GetValues(typeof(Ranks))
                .Cast<Ranks>()
                .Select(e => e.GetDescription());

            RuleFor(p => p.SzRank)
                .NotEmpty().WithMessage("Player needs a splat zone rank.")
                .Must(rank => 
                {
                    return ranks.Contains(rank);
                })
                .WithMessage("Invalid splat zones rank.");

            RuleFor(p => p.TcRank)
                .NotEmpty().WithMessage("Player needs a tower control rank.")
                .Must(rank =>
                {
                    return ranks.Contains(rank);
                })
                .WithMessage("Invalid tower control rank.");

            RuleFor(p => p.RmRank)
                .NotEmpty().WithMessage("Player needs a rain maker rank.")
                .Must(rank =>
                {
                    return ranks.Contains(rank);
                })
                .WithMessage("Invalid rain maker rank.");

            RuleFor(p => p.CbRank)
                .NotEmpty().WithMessage("Player needs a clam blitz rank")
                .Must(rank =>
                {
                    return ranks.Contains(rank);
                })
                .WithMessage("Invalid clam blitz rank.");

            RuleFor(p => p.IsActive)
                .NotNull().WithMessage("player needs an active status");

            RuleFor(e => e)
                 .MustAsync(IsValidTeamId)
                 .WithMessage("Team does not exist in the database.")
                 .MustAsync(IsValidPlayerId)
                 .WithMessage("Player does not exist in the database.");
        }

        private async Task<bool> IsValidTeamId(UpdatePlayerCommand e, CancellationToken token)
        {
            if (e.TeamId == null)
            {
                return true;
            }

            return await this._teamRepository.DoesTeamExist((int)e.TeamId);
        }

        private async Task<bool> IsValidPlayerId(UpdatePlayerCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._playerRepository.DoesPlayerExist(e.Id);
        }
    }
}
