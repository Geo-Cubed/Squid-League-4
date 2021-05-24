namespace GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.CreateHelpfulPerson
{
    public class HelpfulPersonDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Description { get; set; }

        public string ProfilePictureLink { get; set; }

        public string TwitterLink { get; set; }

        public string Role { get; set; }
    }
}
