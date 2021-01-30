using Prism.Commands;
using Prism.Mvvm;
using SquidLeagueAdmin.Models;
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
using Switch = SquidLeagueAdmin.Models.SystemSwitch; 

namespace SquidLeagueAdmin.UI.ViewModels.GameSettings
{
    public class GameSettingViewModel : BindableBase
    {
        #region Constructor and Private variables
        private GameSetting gameSetting;
        private ObservableCollection<GameSetting> currentSettings;
        private List<GameSetting> allSettings;

        private IRepository<GameSetting> settingRepo;
        private IRepository<Map> mapRepo;
        private IRepository<Switch> switchRepo;

        private List<Switch> allSwitches;

        private ObservableCollection<string> brackets;
        private string selectedBracket;

        private ObservableCollection<string> swissStages;
        private ObservableCollection<string> knockoutStages;
        private ObservableCollection<Map> maps;

        private ObservableCollection<GameModes> modes;
        private GameModes selectedMode;

        private ObservableCollection<int> swissSortOrders;
        private ObservableCollection<int> knockoutSortOrders;

        private string lblText;
        private string lblColour;

        public GameSettingViewModel()
        {
            SaveCommand = new DelegateCommand(SaveAsync, () => true);
            ReloadCommand = new DelegateCommand(ReloadAsync, () => true);
            DeleteCommand = new DelegateCommand(DeleteAsync, () => true);

            this.LabelColour = "Green";
            this.gameSetting = new GameSetting();
            this.settingRepo = RepositoryFactory.GetGameSettingRepository("SQL");
            this.mapRepo = RepositoryFactory.GetMapRepository("SQL");
            this.switchRepo = RepositoryFactory.GetSystemSwitchRepository("SQL");

            this.LoadMaps();
            this.LoadSwitches();
            this.LoadBrackets();
            this.LoadGameModes();
            this.LoadSettings();
        }
        #endregion

        #region Methods
        private async void LoadMaps()
        {
            this.Maps = new ObservableCollection<Map>() 
            { 
                new Map() { Id = -1, Name = "No Map" } 
            };

            var data = await Task.Run(() => this.mapRepo.GetItems());
            foreach (var item in data)
            {
                this.Maps.Add(item);
            }
        }

        private async void LoadSwitches()
        {
            this.allSwitches = new List<Switch>();

            var data = await Task.Run(() => this.switchRepo.GetItems());
            foreach (var item in data)
            {
                this.allSwitches.Add(item);
            }
        }

        private async void LoadSettings()
        {
            this.allSettings = new List<GameSetting>();

            var data = await Task.Run(() => this.settingRepo.GetItems());
            foreach (var item in data)
            {
                this.allSettings.Add(item);
            }
        }

        private async void LoadBrackets()
        {
            this.Brackets = new ObservableCollection<string>()
            {
                "Swiss",
                "Knockout"
            };
        }

        private async void LoadGameModes()
        {
            this.Modes = new ObservableCollection<GameModes>()
            {
                GameModes.Undefined,
                GameModes.ClamBlitz,
                GameModes.RainMaker,
                GameModes.SplatZones,
                GameModes.TowerControl,
                GameModes.TurfWar
            };
        }
        #endregion

        #region Delegates
        public DelegateCommand SaveCommand { get; }

        public DelegateCommand ReloadCommand { get; }

        public DelegateCommand DeleteCommand { get; }
        #endregion

        #region Delegate Commands
        private async void SaveAsync()
        {

        }

        private async void ReloadAsync()
        {

        }

        private async void DeleteAsync()
        {

        }
        #endregion

        #region Bindables
        public ObservableCollection<string> Brackets
        {
            get => this.brackets;
            set => SetProperty(ref this.brackets, value);
        }

        public string SelectedBracket
        {
            get => this.selectedBracket;
            set 
            {
                SetProperty(ref this.selectedBracket, value);
                // TODO: Logic to swap out other combo boxes
            }
        }

        public ObservableCollection<GameModes> Modes
        {
            get => this.modes;
            set => SetProperty(ref this.modes, value);
        }

        public GameModes SelectedMode
        {
            get => this.selectedMode;
            set => SetProperty(ref this.selectedMode, value);
        }

        public ObservableCollection<Map> Maps
        {
            get => this.maps;
            set => SetProperty(ref this.maps, value);
        }

        public Map SelectedMap
        {
            get
            {
                if (this.Maps.Where(x => x.Id == this.gameSetting.Id).Any())
                {
                    return this.Maps.Where(x => x.Id == this.gameSetting.Id).First();
                }

                return this.Maps.ElementAt(0);
            }
            set => SetProperty(ref this.gameSetting.MapId, value.Id);
        }

        public string LabelText
        {
            get => this.lblText;
            set => SetProperty(ref this.lblText, value);
        }

        public string LabelColour
        {
            get => this.lblColour;
            set => SetProperty(ref this.lblColour, value);
        }
        #endregion

        async void DisplayLabelAsync(string message, int timeDelay, bool IsError = false)
        {
            this.LabelColour = (IsError) ? "red" : "green";
            this.LabelText = message;
            await Task.Run(() => Thread.Sleep(timeDelay * 1000));
            this.LabelText = string.Empty;
        }
    }
}
