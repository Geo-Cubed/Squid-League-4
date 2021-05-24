using FluentValidation;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.DeleteTeam
{
    internal class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
    {
        private ITeamRepository _teamRepository;

        public DeleteTeamCommandValidator(ITeamRepository teamRepository)
        {
            this._teamRepository = teamRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(teamRepository.GetType(), this.GetType()));

            RuleFor(e => e)
                .MustAsync(IsValidTeamId)
                .WithMessage("Team does not exist");
        }

        private async Task<bool> IsValidTeamId(DeleteTeamCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._teamRepository.DoesTeamExist(e.Id);
        }
    }
}