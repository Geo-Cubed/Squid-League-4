using SquidLeagueAdmin.UI.ViewModels.Casters;
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

namespace SquidLeagueAdmin.UI.Views.Casters
{
    /// <summary>
    /// Interaction logic for CasterPage.xaml
    /// </summary>
    public partial class CasterPage : Page
    {
        public CasterPage()
        {
            InitializeComponent();
            DataContext = new CasterViewModel();
        }
    }
}
