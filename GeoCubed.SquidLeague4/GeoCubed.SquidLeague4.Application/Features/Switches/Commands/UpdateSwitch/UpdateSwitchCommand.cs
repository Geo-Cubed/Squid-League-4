using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Commands.UpdateSwitch
{
    public class UpdateSwitchCommand : IRequest<UpdateSwitchCommandResponse>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}