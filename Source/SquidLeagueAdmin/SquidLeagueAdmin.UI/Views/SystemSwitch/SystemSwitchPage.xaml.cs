using SquidLeagueAdmin.UI.ViewModels.SystemSwitch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SquidLeagueAdmin.UI.Views.SystemSwitch
{
    /// <summary>
    /// Interaction logic for SystemSwitchPage.xaml
    /// </summary>
    public partial class SystemSwitchPage : Page
    {
        public SystemSwitchPage()
        {
            InitializeComponent();
            DataContext = new SystemSwitchViewModel();
        }
    }
}
