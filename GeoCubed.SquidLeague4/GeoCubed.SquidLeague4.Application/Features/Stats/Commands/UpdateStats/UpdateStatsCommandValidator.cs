using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Commands.UpdateStats
{
    internal class UpdateStatsCommandValidator : AbstractValidator<UpdateStatsCommand>
    {
        private IStatisticRepository _statsRepository;

        public UpdateStatsCommandValidator(IStatisticRepository statsRepository)
        {
            this._statsRepository = statsRepository;

            RuleFor(e => e.Alias)
                .NotEmpty().WithMessage("Alias cannot be empty")
                .MaximumLength(128).WithMessage("Alias cannot be over 128 characters");

            RuleFor(e => e.Sql)
                .NotEmpty().WithMessage("Sql cannot be empty");

            RuleFor(e => e)
                .MustAsync(DoesStatExist).WithMessage("Stat must exist")
                .MustAsync(IsAliasUnique).WithMessage("Alias must be unique");
        }

        private async Task<bool> DoesStatExist(UpdateStatsCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return !(await this._statsRepository.DoesStatExist(e.Id));
        }

        private async Task<bool> IsAliasUnique(UpdateStatsCommand e, CancellationToken token)
        {
            return await this._statsRepository.IsAliasUnique(e.Id, e.Alias);
        }
    }
}