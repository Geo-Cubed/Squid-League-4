using SquidLeagueAdmin.UI.ViewModels.Players;
using System.Windows.Controls;

namespace SquidLeagueAdmin.UI.Views.Players
{
    /// <summary>
    /// Interaction logic for PlayerPage.xaml
    /// </summary>
    public partial class PlayerPage : Page
    {
        public PlayerPage()
        {
            InitializeComponent();
            DataContext = new PlayerViewmodel();
        }
    }
}
