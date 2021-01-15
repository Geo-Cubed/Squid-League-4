using SquidLeagueAdmin.UI.ViewModels.HelpfulPeople;
using System.Windows.Controls;

namespace SquidLeagueAdmin.UI.Views.HelpfulPeople
{
    /// <summary>
    /// Interaction logic for HelpfulPeoplePage.xaml
    /// </summary>
    public partial class HelpfulPeoplePage : Page
    {
        public HelpfulPeoplePage()
        {
            InitializeComponent();
            DataContext = new HelpfulPeopleViewModel();
        }
    }
}
