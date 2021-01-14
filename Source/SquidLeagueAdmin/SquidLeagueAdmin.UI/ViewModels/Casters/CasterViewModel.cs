using Prism.Commands;
using Prism.Mvvm;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SquidLeagueAdmin.UI.ViewModels.Casters
{
    public class CasterViewModel : BindableBase
    {
        #region Constructor and private variables
        private Caster Model;
        private IRepository<Caster> casterRepo;
        private ObservableCollection<Caster> allCasters;
        private string labelText;
        private string labelColour;

        public CasterViewModel()
        {
            saveCommand = new DelegateCommand(this.SaveAsync, () => true);
            reloadCommand = new DelegateCommand(this.ReloadAsync, () => true);
            deleteCommand = new DelegateCommand(this.DeleteAsync, () => true);

            this.casterRepo = RepositoryFactory.GetCasterRepository("SQL");
            this.Model = new Caster();
            this.LoadDataAsync();
            this.labelColour = "green";
        }
        #endregion

        #region public methods
        public async void LoadDataAsync()
        {
            this.casters = new ObservableCollection<Caster>();
            this.casters.Add(new Caster() { Id = -1, Name = "New Caster", IsActive = 0});

            var data = await Task.Run(() => this.casterRepo.GetItems());
            foreach (var item in data)
            {
                this.casters.Add(item);
            }
        }
        #endregion

        #region delegates
        public DelegateCommand saveCommand { get; }

        public DelegateCommand reloadCommand { get; }

        public DelegateCommand deleteCommand { get; }
        #endregion

        #region delegate commands
        public async void SaveAsync()
        {
            if (this.Model == null)
            {
                return;
            }

            if (this.Model.Name == null || this.Model.Name.Trim() == string.Empty)
            {
                this.DisplayLabelAsync("Caster must have a name", 2, true);
                return;
            }

            if (this.Model.Id == 0)
            {
                this.Model.Id = -1;
            }

            if (this.Model.Id < 0)
            {
                try
                {
                    await Task.Run(() => this.casterRepo.AddItem(this.Model));
                    this.DisplayLabelAsync($"{this.Model.Name} created successfully", 2);
                }
                catch
                {
                    this.DisplayLabelAsync($"There was an error while trying to create {this.Model.Name}", 2, true);
                    return;
                }
            }
            else
            {
                try
                {
                    await Task.Run(() => this.casterRepo.UpdateItem(this.Model));
                    this.DisplayLabelAsync($"{this.Model.Name} updated successfully", 2);
                }
                catch
                {
                    this.DisplayLabelAsync($"There was an issue while trying to update {this.Model.Name}", 2, true);
                    return;
                }
            }

            this.LoadDataAsync();
        }

        public async void ReloadAsync()
        {
            this.LoadDataAsync();
        }

        public async void DeleteAsync()
        {
            if (this.Model == null)
            {
                return;
            }

            if (this.Model.Id < 0)
            {
                this.DisplayLabelAsync("You cannot delete a caster that does not exist", 2, true);
                return;
            }

            if (MessageBox.Show($"Permenantly Delete {this.Model.Name}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (await Task.Run(() => this.casterRepo.DeleteItem(this.Model)))
                {
                    this.DisplayLabelAsync($"Caster {this.Model.Name} Deleted Successfully", 2);
                    this.ReloadAsync();
                }
                else
                {
                    this.DisplayLabelAsync($"Cannot delete caster {this.Model.Name}", 2, true);
                }
            }
        }
        #endregion

        #region bindings
        public ObservableCollection<Caster> casters
        {
            get => this.allCasters;
            set => SetProperty(ref this.allCasters, value);
        }

        public Caster selectedCaster
        {
            get => this.Model;
            set
            {
                if (value == null)
                {
                    this.Model.Id = -1;
                    this.name = string.Empty;
                    this.twitter = string.Empty;
                    this.twitch = string.Empty;
                    this.youtube = string.Empty;
                    this.discord = string.Empty;
                    this.profilePicture = string.Empty;
                    this.isActive = false;
                }
                else
                {
                    this.Model.IsActive = value.Id;
                    this.name = value.Name;
                    this.twitter = value.Twitter;
                    this.youtube = value.Youtube;
                    this.twitch = value.Twitch;
                    this.discord = value.Discord;
                    this.profilePicture = value.ProfilePicture;
                    this.isActive = (value.IsActive == 1) ? true : false;
                }
            }
        }

        public string name
        {
            get => this.Model.Name;
            set => SetProperty(ref this.Model.Name, value);
        }

        public string twitter
        {
            get => this.Model.Twitter;
            set => SetProperty(ref this.Model.Twitter, value);
        }

        public string youtube
        {
            get => this.Model.Youtube;
            set => SetProperty(ref this.Model.Youtube, value);
        }

        public string twitch
        {
            get => this.Model.Twitch;
            set => SetProperty(ref this.Model.Twitch, value);
        }

        public string discord
        {
            get => this.Model.Discord;
            set => SetProperty(ref this.Model.Discord, value);
        }

        public string profilePicture
        {
            get => this.Model.ProfilePicture;
            set => SetProperty(ref this.Model.ProfilePicture, value);
        }

        public bool isActive
        {
            get => (this.Model.IsActive == 1) ? true : false;
            set => SetProperty(ref this.Model.IsActive, (value)? 1 : 0);
        }

        public string lblText
        {
            get => this.labelText;
            set => SetProperty(ref this.labelText, value);
        }

        public string lblColour
        {
            get => this.labelColour;
            set => SetProperty(ref this.labelColour, value);
        }
        #endregion

        async void DisplayLabelAsync(string message, int timeDelay, bool IsError = false)
        {
            this.labelColour = (IsError) ? "red" : "green";
            this.labelText = message;
            await Task.Run(() => Thread.Sleep(timeDelay * 1000));
            this.labelText = string.Empty;
        }
    }
}
