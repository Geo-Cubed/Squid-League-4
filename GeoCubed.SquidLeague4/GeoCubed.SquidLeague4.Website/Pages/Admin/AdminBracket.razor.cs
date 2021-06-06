using GeoCubed.SquidLeague4.Website.Models.Enums;
using Microsoft.AspNetCore.Components;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminBracket
    {
        protected BracketFormats selectedFormat { get; set; }
            = BracketFormats.Swiss;

        protected void OnBracketSelect(ChangeEventArgs e)
        {
            this.selectedFormat = (BracketFormats)int.Parse(e.Value.ToString());
        }
    }
}
