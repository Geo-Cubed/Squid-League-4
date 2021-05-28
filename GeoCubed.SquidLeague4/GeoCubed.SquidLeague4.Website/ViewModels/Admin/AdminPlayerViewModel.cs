namespace GeoCubed.SquidLeague4.Website.ViewModels.Admin
{
    public class AdminPlayerViewModel
    {
        public int Id { get; set; }

        public string InGameName { get; set; }

        public string SzRank { get; set; }

        public string RmRank { get; set; }

        public string TcRank { get; set; }

        public string CbRank { get; set; }

        public int? TeamId { get; set; }

        public bool IsActive { get; set; }
    }
}
