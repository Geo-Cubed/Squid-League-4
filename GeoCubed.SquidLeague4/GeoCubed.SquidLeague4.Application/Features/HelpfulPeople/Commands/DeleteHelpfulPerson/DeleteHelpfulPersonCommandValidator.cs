using FluentValidation;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.DeleteHelpfulPerson
{
    internal class DeleteHelpfulPersonCommandValidator : AbstractValidator<DeleteHelpfulPersonCommand>
    {
        private IHelpfulPersonRepository _helpfulPersonRepository;

        public DeleteHelpfulPersonCommandValidator(IHelpfulPersonRepository helpfulPersonRepository)
        {
            this._helpfulPersonRepository = helpfulPersonRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(helpfulPersonRepository.GetType(), this.GetType()));

            RuleFor(e => e)
                .MustAsync(IsValidPersonId)
                .WithMessage("Helpflul person does not exist");
        }

        private async Task<bool> IsValidPersonId(DeleteHelpfulPersonCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._helpfulPersonRepository.DoesHelpfulPersonExist(e.Id);
        }
    }
}