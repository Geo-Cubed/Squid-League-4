using SquidLeagueAdmin.UI.Views;
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

        }

        private void BtnBracket_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMatchInsert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnTeams_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPlayers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMaps_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCasters_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdmins_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnWeapons_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGameSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
