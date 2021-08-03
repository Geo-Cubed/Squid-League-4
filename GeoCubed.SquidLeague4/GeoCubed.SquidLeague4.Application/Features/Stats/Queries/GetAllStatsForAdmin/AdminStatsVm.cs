namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetAllStatsForAdmin
{
    public class AdminStatsVm
    {
        public int Id { get; set; }

        public string Alias { get; set; }

        public string Sql { get; set; }

        public string Modifier { get; set; }
    }
}