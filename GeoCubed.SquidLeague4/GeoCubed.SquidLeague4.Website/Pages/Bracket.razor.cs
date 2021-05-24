using GeoCubed.SquidLeague4.Website.Models.Enums;
using Microsoft.AspNetCore.Components;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    partial class Bracket
    {
        public BracketFormats SelectedFormat { get; set; }
            = BracketFormats.Swiss;

        protected void OnBracketSelect(ChangeEventArgs e)
        {
            this.SelectedFormat = (BracketFormats)int.Parse(e.Value.ToString());
        }
    }
}
