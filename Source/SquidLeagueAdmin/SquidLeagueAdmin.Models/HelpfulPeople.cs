namespace SquidLeagueAdmin.Models
{
    public class HelpfulPeople
    {
        public int Id;

        public string UserName;

        public string Description;

        public string ProfilePicture;

        public string Twitter;

        public override string ToString()
        {
            return this.UserName;
        }
    }
}
