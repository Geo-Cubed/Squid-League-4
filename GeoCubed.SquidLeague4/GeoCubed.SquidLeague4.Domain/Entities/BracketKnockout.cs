namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class BracketKnockout
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public string Stage { get; set; }

        public virtual Match Match { get; set; }
    }
}
