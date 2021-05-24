using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.UpdateHelpfulPerson
{
    public class UpdateHelpfulPersonCommand : IRequest<UpdateHelpfulPersonCommandResponse>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Description { get; set; }

        public string ProfilePictureLink { get; set; }

        public string TwitterLink { get; set; }

        public string Role { get; set; }
    }
}