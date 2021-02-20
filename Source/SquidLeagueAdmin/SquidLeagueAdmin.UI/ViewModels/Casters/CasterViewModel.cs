using Prism.Commands;
using Prism.Mvvm;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.Models.Enums;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private int casterIndex;
        private string labelText;
        private string labelColour;

        public CasterViewModel()
        {
            saveCommand = new DelegateCommand(this.SaveAsync, () => true);
            reloadCommand = new DelegateCommand(this.ReloadAsync, () => true);
            deleteCommand = new DelegateCommand(this.DeleteAsync, () => true);

            this.casterRepo = RepositoryFactory.GetCasterRepository(RepositoryTypes.Database);
            this.Model = new Caster();
            this.casterIndex = 0;
            this.LoadDataAsync();
            this.labelColour = "green";
        }
        #endregion

        #region public methods
        public async void LoadDataAsync(int lastId = -1)
        {
            this.casters = new ObservableCollection<Caster>()
            {
                new Caster() { Id = -1, Name = "New Caster", IsActive = 0}
            };

            //this.SelectedCasterIndex = 0;
            var data = await Task.Run(() => this.casterRepo.GetItems());
            foreach (var item in data)
            {
                this.casters.Add(item);
            }

            this.TryLoadPreviousModel(lastId);
        }

        public void TryLoadPreviousModel(int lastId)
        {
            if (lastId <= 0)
            {
                this.SelectedCasterIndex = 0;
                return;
            }

            var found = false;
            var index = 0;
            foreach (var item in this.casters)
            {
                if (item.Id == lastId)
                {
                    found = true;
                    break;
                }
                
                ++index;
            }

            if (found)
            {
                this.SelectedCasterIndex = index;
            }
            else
            {
                this.SelectedCasterIndex = 0;
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
                if (await Task.Run(() => this.casterRepo.AddItem(this.Model)))
                {                  
                    this.DisplayLabelAsync($"{this.Model.Name} created successfully", 2);
                }
                else
                {
                    this.DisplayLabelAsync($"There was an error while trying to create {this.Model.Name}", 2, true);
                    return;
                }
            }
            else
            {
                if (await Task.Run(() => this.casterRepo.UpdateItem(this.Model)))
                {                 
                    this.DisplayLabelAsync($"{this.Model.Name} updated successfully", 2);
                }
                else
                {
                    this.DisplayLabelAsync($"There was an issue while trying to update {this.Model.Name}", 2, true);
                    return;
                }
            }

            var lastId = Model.Id;
            this.LoadDataAsync(lastId);
        }

        public async void ReloadAsync()
        {
            var lastId = Model.Id;
            this.LoadDataAsync(lastId);
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
                    this.Model.Id = value.Id;
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

        public int SelectedCasterIndex
        {
            get => this.casterIndex;
            set
            {
                if (value < 0 || value > (this.casters.Count() + 1))
                {
                    SetProperty(ref this.casterIndex, 0);
                }
                else
                {
                    SetProperty(ref this.casterIndex, value);
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
            this.lblColour = (IsError) ? "red" : "green";
            this.lblText = message;
            await Task.Run(() => Thread.Sleep(timeDelay * 1000));
            this.lblText = string.Empty;
        }
    }
}
