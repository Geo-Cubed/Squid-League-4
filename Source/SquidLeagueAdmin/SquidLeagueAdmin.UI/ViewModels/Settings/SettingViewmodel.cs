using Prism.Commands;
using Prism.Mvvm;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SquidLeagueAdmin.UI.ViewModels.Settings
{
    public class SettingViewModel : BindableBase
    {
        public Config Model { get; set; }

        private IRepository<Config> repo;

        private string savedText;

        public SettingViewModel()
        {
            SaveCommand = new DelegateCommand<PasswordBox>(SaveAsync);

            try
            {
                this.repo = RepositoryFactory.GetConfigRepository("JSON");
                this.Model = this.repo.GetItems().FirstOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string DatabaseName
        {
            get => Model.DatabaseName;
            set => SetProperty(ref Model.DatabaseName, value);
        }

        public string DatabaseAddress
        {
            get => Model.Address;
            set => SetProperty(ref Model.Address, value);
        }

        public string DatabasePort
        {
            get => Model.Port;
            set => SetProperty(ref Model.Port, value);
        }

        public string UserName
        {
            get => Model.Username;
            set => SetProperty(ref Model.Username, value);
        }

        public string LabelText
        {
            get => this.savedText;
            set => SetProperty(ref savedText, value);
        }

        public DelegateCommand<PasswordBox> SaveCommand { get; }

        async void SaveAsync(PasswordBox passBox)
        {
            this.Model.Password = passBox.Password;
            await Task.Run(() => this.repo.AddItem(this.Model));
            this.LabelText = "Settings Saved";
            this.RevertLabel(2);
        }

        async void RevertLabel(int timeDelay)
        {
            await Task.Run(() => Thread.Sleep(timeDelay * 1000));
            this.LabelText = string.Empty;
        }
    }
}
