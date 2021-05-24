namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class User
    {
        public int Id { get; set; }

        public int Username { get; set; }

        public string PasswordHash { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
