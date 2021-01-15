using Prism.Commands;
using Prism.Mvvm;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SquidLeagueAdmin.UI.ViewModels.Maps
{
    public class MapViewModel : BindableBase
    {
        #region Constructor and private variables
        private Map Model;
        private ObservableCollection<Map> allMaps;
        private IRepository<Map> mapRepo;
        private string labelText;
        private string labelColour;
        public MapViewModel()
        {
            saveCommand = new DelegateCommand(SaveAsync, () => true);
            reloadCommand = new DelegateCommand(ReloadAsync, () => true);

            this.mapRepo = RepositoryFactory.GetMapRepository("SQL");
            this.Model = new Map() { Id = -1 };
            this.lblColour = "green";
            this.LoadDataAsync();
        }
        #endregion

        #region Public methods
        public async void LoadDataAsync()
        {
            this.maps = new ObservableCollection<Map>();

            var data = await Task.Run(() => this.mapRepo.GetItems());
            foreach (var item in data)
            {
                this.maps.Add(item);
            }
        }
        #endregion

        #region Delegates
        public DelegateCommand saveCommand { get; }

        public DelegateCommand reloadCommand { get; }
        #endregion

        #region Delegate Commands
        public async void SaveAsync()
        {
            if (this.Model == null)
            {
                return;
            }

            if (this.Model.Id <= 0)
            {
                this.DisplayLabelAsync("Cannot  edit a map that doesn't exist", 2, true);
                return;
            }

            if (await Task.Run(() => this.mapRepo.UpdateItem(this.Model)))
            {
                this.DisplayLabelAsync($"Successfully updated {this.Model.Name}", 2);
                this.LoadDataAsync();
            }
            else
            {
                this.DisplayLabelAsync($"There was an issue while trying to update {this.Model.Name}", 2, true);
            }
        }

        public async void ReloadAsync()
        {
            this.LoadDataAsync();
        }
        #endregion

        #region Bindables
        public ObservableCollection<Map> maps
        {
            get => this.allMaps;
            set => SetProperty(ref this.allMaps, value);
        }

        public Map selectedMap
        {
            get => this.Model;
            set
            {
                if (value == null)
                {
                    this.Model.Id = -1;
                    this.name = "No Map Selected";
                    this.picturePath = string.Empty;
                }
                else
                {
                    this.Model.Id = value.Id;
                    this.name = value.Name;
                    this.picturePath = value.PicturePath;
                }
            }
        }

        public string name
        {
            get => this.Model.Name;
            set => SetProperty(ref this.Model.Name, value);
        }

        public string picturePath
        {
            get => this.Model.PicturePath;
            set => SetProperty(ref this.Model.PicturePath, value);
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
