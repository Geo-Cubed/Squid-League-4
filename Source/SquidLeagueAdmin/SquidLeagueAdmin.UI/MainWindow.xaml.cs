using SquidLeagueAdmin.UI.Views;
using SquidLeagueAdmin.UI.Views.Casters;
using SquidLeagueAdmin.UI.Views.GameSettings;
using SquidLeagueAdmin.UI.Views.HelpfulPeople;
using SquidLeagueAdmin.UI.Views.Maps;
using SquidLeagueAdmin.UI.Views.Matches;
using SquidLeagueAdmin.UI.Views.Players;
using SquidLeagueAdmin.UI.Views.Results;
using SquidLeagueAdmin.UI.Views.Settings;
using SquidLeagueAdmin.UI.Views.Teams;
using SquidLeagueAdmin.UI.Views.Weapons;
using System.Windows;

namespace SquidLeagueAdmin.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainContent.Content = new HomePage();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new HomePage();
        }

        private void BtnMatchInsert_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new MatchPage();
        }

        private void BtnResults_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new ResultPage();
        }

        private void BtnTeams_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new TeamPage();
        }

        private void BtnPlayers_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new PlayerPage();
        }

        private void BtnMaps_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new MapPage();
        }

        private void BtnCasters_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new CasterPage();
        }

        private void BtnWeapons_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new WeaponPage();
        }

        private void BtnGameSettings_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new GameSettingPage();
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new SettingPage();
        }

        private void BtnHelpfulPeople_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new HelpfulPeoplePage();
        }
    }
}
