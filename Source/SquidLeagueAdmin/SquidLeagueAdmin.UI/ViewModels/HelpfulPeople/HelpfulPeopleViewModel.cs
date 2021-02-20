using Prism.Commands;
using Prism.Mvvm;
using SquidLeagueAdmin.Models.Enums;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HelpfulPerson = SquidLeagueAdmin.Models.HelpfulPeople;

namespace SquidLeagueAdmin.UI.ViewModels.HelpfulPeople
{
    public class HelpfulPeopleViewModel : BindableBase
    {
        #region Constructor and Private variables
        private HelpfulPerson Model;
        private ObservableCollection<HelpfulPerson> allPeople;
        private IRepository<HelpfulPerson> personRepo;
        private int selectedIndex;
        private string labelText;
        private string labelColour;

        public HelpfulPeopleViewModel()
        {
            saveCommand = new DelegateCommand(SaveAsync, () => true);
            reloadCommand = new DelegateCommand(ReloadAsync, () => true);
            deleteCommand = new DelegateCommand(DeleteAsync, () => true);

            this.personRepo = RepositoryFactory.GetHelpfulPersonRepository(RepositoryTypes.Database);
            this.lblColour = "green";
            this.Model = new HelpfulPerson() { Id = -1 };
            this.LoadDataAsync();
        }
        #endregion

        #region Public methods
        public async void LoadDataAsync(int lastId = -1)
        {
            this.helpfulPeopleItems = new ObservableCollection<HelpfulPerson>();
            this.helpfulPeopleItems.Add(new HelpfulPerson() { Id = -1, UserName = "New Person" });

            //this.selectedPersonIndex = 0;
            var data = await Task.Run(() => this.personRepo.GetItems());
            foreach (var item in data)
            {
                this.helpfulPeopleItems.Add(item);
            }

            this.TryLoadPreviousModel(lastId);
        }

        public void TryLoadPreviousModel(int lastId)
        {
            if (lastId <= 0)
            {
                this.selectedPersonIndex = 0;
                return;
            }

            var found = false;
            var index = 0;
            foreach (var item in this.helpfulPeopleItems)
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
                this.selectedPersonIndex = index;
            }
            else
            {
                this.selectedPersonIndex = 0;
            }
        }

        #endregion

        #region Delegates
        public DelegateCommand saveCommand { get; }

        public DelegateCommand reloadCommand { get; }

        public DelegateCommand deleteCommand { get; }
        #endregion

        #region Delegate Commands
        public async void SaveAsync()
        {
            if (this.Model == null)
            {
                return;
            }

            if (this.Model.UserName == null || this.Model.UserName.Trim() == string.Empty)
            {
                this.DisplayLabelAsync("Person must have a name", 2, true);
                return;
            }

            if (this.Model.Id == 0)
            {
                this.Model.Id = -1;
            }

            if (this.Model.Id < 0)
            {
                if (await Task.Run(() => this.personRepo.AddItem(this.Model)))
                {
                    this.DisplayLabelAsync($"{this.Model.UserName} created successfully", 2);
                }
                else
                {
                    this.DisplayLabelAsync($"There was an error while trying to create {this.Model.UserName}", 2, true);
                    return;
                }
            }
            else
            {
                if (await Task.Run(() => this.personRepo.UpdateItem(this.Model)))
                {
                    this.DisplayLabelAsync($"{this.Model.UserName} updated successfully", 2);
                }
                else
                {
                    this.DisplayLabelAsync($"There was an issue while trying to update {this.Model.UserName}", 2, true);
                    return;
                }
            }

            var lastId = this.Model.Id;
            this.LoadDataAsync(lastId);
        }

        public async void ReloadAsync()
        {
            var lastId = this.Model.Id;
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
                this.DisplayLabelAsync("You cannot delete a person that does not exist", 2, true);
                return;
            }

            if (MessageBox.Show($"Permenantly Delete {this.Model.UserName}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (await Task.Run(() => this.personRepo.DeleteItem(this.Model)))
                {
                    this.DisplayLabelAsync($"Person {this.Model.UserName} Deleted Successfully", 2);
                    this.ReloadAsync();
                }
                else
                {
                    this.DisplayLabelAsync($"Cannot delete person {this.Model.UserName}", 2, true);
                }
            }
        }
        #endregion

        #region Bindings
        public ObservableCollection<HelpfulPerson> helpfulPeopleItems
        {
            get => this.allPeople;
            set => SetProperty(ref this.allPeople, value);
        }

        public HelpfulPerson selectedPerson
        {
            get => this.Model;
            set
            {
                if (value == null)
                {
                    this.Model.Id = -1;
                    this.name = string.Empty;
                    this.description = string.Empty;
                    this.profilePicture = string.Empty;
                    this.twitter = string.Empty;
                }
                else
                {
                    this.Model.Id = value.Id;
                    this.name = value.UserName;
                    this.description = value.Description;
                    this.profilePicture = value.ProfilePicture;
                    this.twitter = value.Twitter;
                }
            }
        }

        public int selectedPersonIndex
        {
            get => this.selectedIndex;
            set
            {
                if (value < 0 || value > (this.helpfulPeopleItems.Count() + 1))
                {
                    SetProperty(ref this.selectedIndex, 0);
                }
                else
                {
                    SetProperty(ref this.selectedIndex, value);
                }
            }
        }

        public string name
        {
            get => this.Model.UserName;
            set => SetProperty(ref this.Model.UserName, value);
        }

        public string description
        {
            get => this.Model.Description;
            set => SetProperty(ref this.Model.Description, value);
        }

        public string profilePicture
        {
            get => this.Model.ProfilePicture;
            set => SetProperty(ref this.Model.ProfilePicture, value);
        }

        public string twitter
        {
            get => this.Model.Twitter;
            set => SetProperty(ref this.Model.Twitter, value);
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
