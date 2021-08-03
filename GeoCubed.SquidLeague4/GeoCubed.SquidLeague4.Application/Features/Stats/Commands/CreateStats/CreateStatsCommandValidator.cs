using FluentValidation;
using GeoCubed.SquidLeague4.Application.Common.Enums;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Commands.CreateStats
{
    internal class CreateStatsCommandValidator : AbstractValidator<CreateStatsCommand>
    {
        private readonly IStatisticRepository _statsRepository;

        public CreateStatsCommandValidator(IStatisticRepository statsRepository)
        {
            this._statsRepository = statsRepository;

            RuleFor(e => e.Alias)
                .NotEmpty().WithMessage("Stat must have a alias")
                .MaximumLength(128).WithMessage("Alias cannot be over 128 characters");

            RuleFor(e => e.Sql)
                .NotEmpty().WithMessage("Stat must have sql");

            RuleFor(e => e.Modifier)
                .Must(IsValidModifier).WithMessage("Invalid modifier");

            RuleFor(e => e)
                .MustAsync(IsAliasUnique).WithMessage("Alias must be unique");
        }

        private async Task<bool> IsAliasUnique(CreateStatsCommand e, CancellationToken token)
        {
            return await this._statsRepository.IsAliasUnique(e.Alias);
        }

        private bool IsValidModifier(string modifier)
        {
            foreach (StatsModifiers modEnum in Enum.GetValues(typeof(StatsModifiers)))
            {
                if (modifier == modEnum.GetDescription())
                {
                    return true;
                }
            }

            return false;
        }
    }
}