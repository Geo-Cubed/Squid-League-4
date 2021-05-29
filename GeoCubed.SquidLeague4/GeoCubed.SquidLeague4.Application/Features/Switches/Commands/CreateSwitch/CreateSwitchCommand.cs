using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Commands.CreateSwitch
{
    public class CreateSwitchCommand : IRequest<CreateSwitchCommandResponse>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}