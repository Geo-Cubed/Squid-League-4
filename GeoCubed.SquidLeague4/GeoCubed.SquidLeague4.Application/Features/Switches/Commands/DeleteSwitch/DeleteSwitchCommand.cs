using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Commands.DeleteSwitch
{
    public class DeleteSwitchCommand : IRequest<DeleteSwitchCommandResponse>
    {
        public int Id { get; set; }
    }
}