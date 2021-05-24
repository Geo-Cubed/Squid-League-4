namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.CreateTeam
{
    public class TeamCommandDto
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public bool IsActive { get; set; }
    }
}
