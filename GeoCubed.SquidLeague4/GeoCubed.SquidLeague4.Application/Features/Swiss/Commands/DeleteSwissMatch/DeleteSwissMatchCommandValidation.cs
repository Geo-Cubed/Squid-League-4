using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Commands.DeleteSwissMatch
{
    internal class DeleteSwissMatchCommandValidation : AbstractValidator<DeleteSwissMatchCommand>
    {
        private ISwissMatchRepository _swissMatchRepository;

        public DeleteSwissMatchCommandValidation(ISwissMatchRepository swissMatchRepository)
        {
            this._swissMatchRepository = swissMatchRepository;

            RuleFor(e => e)
                .MustAsync(DoesSwissMatchExist).WithMessage("No swiss match with that id.");
        }

        private async Task<bool> DoesSwissMatchExist(DeleteSwissMatchCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._swissMatchRepository.DoesSwissMatchExist(e.Id);
        }
    }
}