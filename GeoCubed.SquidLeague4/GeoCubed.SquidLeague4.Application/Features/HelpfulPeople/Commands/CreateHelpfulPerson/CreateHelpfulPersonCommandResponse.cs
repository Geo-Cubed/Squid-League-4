using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.CreateHelpfulPerson
{
    public class CreateHelpfulPersonCommandResponse : BaseResponse
    {
        public CreateHelpfulPersonCommandResponse() : base()
        {
        }

        public HelpfulPersonDto HelpfulPerson { get; set; }
    }
}