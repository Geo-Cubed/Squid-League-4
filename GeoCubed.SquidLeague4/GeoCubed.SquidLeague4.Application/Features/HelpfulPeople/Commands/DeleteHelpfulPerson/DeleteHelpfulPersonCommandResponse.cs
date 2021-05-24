using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.DeleteHelpfulPerson
{
    public class DeleteHelpfulPersonCommandResponse : BaseResponse
    {
        public DeleteHelpfulPersonCommandResponse() : base()
        {
        }

        public int? HelpfulPersonId { get; set; }
    }
}