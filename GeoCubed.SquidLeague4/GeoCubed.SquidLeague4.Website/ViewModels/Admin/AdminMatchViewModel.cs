using System;

namespace GeoCubed.SquidLeague4.Website.ViewModels.Admin
{
    public class AdminMatchViewModel
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public string Winner { get; set; }

        public int? HomeTeamScore { get; set; }

        public int? AwayTeamScore { get; set; }

        public int? CasterProfileId { get; set; }

        public string MatchVodLink { get; set; }

        public DateTime? MatchDate { get; set; }

        public DateTime? DateComponent 
        { 
            get
            {
                if (this.MatchDate == null)
                {
                    return null;
                }

                return this.MatchDate.Value.Date;
            }
            set 
            {
                if (value == null)
                {
                    return;
                }

                if (this.MatchDate == null)
                {
                    this.MatchDate = DateTime.Now.Date;
                }

                var time = this.MatchDate.Value.TimeOfDay;
                var overallDate = value.Value.Add(time);
                this.MatchDate = overallDate;
            } 
        }

        public DateTime? TimeComponent
        {
            get
            {
                if (this.MatchDate == null)
                {
                    return null;
                }

                return this.MatchDate.Value;
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                if (this.MatchDate == null)
                {
                    this.MatchDate = DateTime.Now.Date;
                }

                var date = this.MatchDate.Value.Date;
                var overallDate = date.Add(value.Value.TimeOfDay);
                this.MatchDate = overallDate;
            }
        }

        public int? SecondaryCasterProfileId { get; set; }
    }
}
