using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.DeleteHelpfulPerson
{
    public class DeleteHelpfulPersonCommand : IRequest<DeleteHelpfulPersonCommandResponse>
    {
        public int Id { get; set; }
    }
}