namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class Audit
    {
        public int Id { get; set; }

        public string TableName { get; set; }

        public string ChangeType { get; set; }

        public string OldRow { get; set; }

        public string NewRow { get; set; }

        public string Username { get; set; }
    }
}
