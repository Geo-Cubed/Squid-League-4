using GeoCubed.SquidLeague4.Application.Responses;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Commands.CreateSwitch
{
    public class CreateSwitchCommandResponse : BaseResponse
    {
        public CreateSwitchCommandResponse() : base()
        {
        }

        public SwitchCommandDto Switch { get; set; }
    }
}