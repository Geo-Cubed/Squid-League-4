namespace SquidLeagueAdmin.Models
{
    public class Map
    {
        public int Id;

        public string Name;

        public string PicturePath;

        public override string ToString()
        {
            return this.Name;
        }
    }
}
