using SquidLeagueAdmin.UI.Views;
using SquidLeagueAdmin.UI.Views.Admins;
using SquidLeagueAdmin.UI.Views.Casters;
using SquidLeagueAdmin.UI.Views.GameSettings;
using SquidLeagueAdmin.UI.Views.Maps;
using SquidLeagueAdmin.UI.Views.Matches;
using SquidLeagueAdmin.UI.Views.Players;
using SquidLeagueAdmin.UI.Views.Results;
using SquidLeagueAdmin.UI.Views.Settings;
using SquidLeagueAdmin.UI.Views.Teams;
using SquidLeagueAdmin.UI.Views.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void BtnBracket_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new ResultPage();
        }

        private void BtnMatchInsert_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new MatchPage();
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

        private void BtnAdmins_Click(object sender, RoutedEventArgs e)
        {
            mainContent.Content = new AdminPage();
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
    }
}
