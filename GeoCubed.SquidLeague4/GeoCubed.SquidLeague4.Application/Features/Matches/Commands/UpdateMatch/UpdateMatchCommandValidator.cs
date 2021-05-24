using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Commands.UpdateMatch
{
    internal class UpdateMatchCommandValidator : AbstractValidator<UpdateMatchCommand>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICasterRepository _casterRepository;
        private readonly IMatchRepository _matchRepository;

        public UpdateMatchCommandValidator(IMatchRepository matchRepository, ITeamRepository teamRepository, ICasterRepository casterRepository)
        {
            this._casterRepository = casterRepository;
            this._teamRepository = teamRepository;
            this._matchRepository = matchRepository;

            RuleFor(m => m.HomeTeamScore)
                .Must(m => m == null || m.Value >= 0).WithMessage("Cannot have a negative home team score");

            RuleFor(m => m.AwayTeamScore)
                .Must(m => m == null || m.Value >= 0).WithMessage("Cannot have a negative away team score");

            RuleFor(e => e)
                .MustAsync(IsValidMatch).WithMessage("Match does not exist")
                .MustAsync(IsValidHomeTeam).WithMessage("Home Team does not exist")
                .MustAsync(IsValidAwayTeam).WithMessage("Away Team does not exist")
                .MustAsync(IsValidPrimaryCaster).WithMessage("Primary Caster does not exist")
                .MustAsync(IsValidSecondaryCaster).WithMessage("Secondary Caster does not exist");
        }

        private async Task<bool> IsValidMatch(UpdateMatchCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._matchRepository.DoesMatchExist(e.Id);
        }

        private async Task<bool> IsValidPrimaryCaster(UpdateMatchCommand e, CancellationToken token)
        {
            return await this.IsValidCasterId(e.CasterProfileId);
        }

        private async Task<bool> IsValidSecondaryCaster(UpdateMatchCommand e, CancellationToken token)
        {
            return await this.IsValidCasterId(e.SecondaryCasterProfileId);
        }

        private async Task<bool> IsValidCasterId(int? casterId)
        {
            if (casterId == null)
            {
                return true;
            }

            if (casterId.Value <= 0)
            {
                return false;
            }

            return await this._casterRepository.DoesCasterExist(casterId.Value);
        }

        private async Task<bool> IsValidHomeTeam(UpdateMatchCommand e, CancellationToken token)
        {
            return await this.IsValidTeamId(e.HomeTeamId);
        }

        private async Task<bool> IsValidAwayTeam(UpdateMatchCommand e, CancellationToken token)
        {
            return await this.IsValidTeamId(e.AwayTeamId);
        }

        private async Task<bool> IsValidTeamId(int teamId)
        {
            if (teamId <= 0)
            {
                return false;
            }

            return await this._teamRepository.DoesTeamExist(teamId);
        }
    }
}