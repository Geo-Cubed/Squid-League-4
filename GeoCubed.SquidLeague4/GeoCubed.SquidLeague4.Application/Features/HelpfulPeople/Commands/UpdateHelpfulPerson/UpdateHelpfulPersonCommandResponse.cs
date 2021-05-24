using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.CreateHelpfulPerson;
using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.UpdateHelpfulPerson
{
    public class UpdateHelpfulPersonCommandResponse : BaseResponse
    {
        public UpdateHelpfulPersonCommandResponse() : base()
        {
        }

        public HelpfulPersonDto HelpfulPerson { get; set; }
    }
}