namespace GeoCubed.SquidLeague4.Application.Features.Casters.Commands.CreateCaster
{
    public class CasterCommandDto
    {
        public int Id { get; set; }

        public string CasterName { get; set; }

        public string Twitter { get; set; }

        public string YouTube { get; set; }

        public string Twitch { get; set; }

        public string Discord { get; set; }

        public string ProfilePicturePath { get; set; }

        public bool IsActive { get; set; }
    }
}
