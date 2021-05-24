namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class BracketSwiss
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public int MatchWeek { get; set; }

        public virtual Match Match { get; set; }
    }
}
