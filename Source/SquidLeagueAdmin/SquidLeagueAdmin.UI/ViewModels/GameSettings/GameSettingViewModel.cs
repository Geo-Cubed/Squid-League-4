using Prism.Commands;
using Prism.Mvvm;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.Models.Enums;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using SquidLeagueAdmin.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Switch = SquidLeagueAdmin.Models.SystemSwitch; 

namespace SquidLeagueAdmin.UI.ViewModels.GameSettings
{
    public class GameSettingViewModel : BindableBase
    {
        #region Constructor and Private variables
        private GameSetting gameSetting;
        private List<GameSetting> allSettings;

        private IRepository<GameSetting> settingRepo;
        private IRepository<Map> mapRepo;
        private IRepository<Switch> switchRepo;

        private List<Switch> allSwitches;

        private ObservableCollection<BracketTypes> brackets;
        private BracketTypes selectedBracket;

        private ObservableCollection<string> stages;

        private ObservableCollection<int> sortOrder;

        private ObservableCollection<Map> maps;
        private Map selectedMap;

        private ObservableCollection<GameModes> modes;
        private GameModes selectedMode;

        private string lblText;
        private string lblColour;

        public GameSettingViewModel()
        {
            SaveCommand = new DelegateCommand(SaveAsync, () => true);
            ReloadCommand = new DelegateCommand(ReloadAsync, () => true);
            DeleteCommand = new DelegateCommand(DeleteAsync, () => true);

            this.LabelColour = "Green";
            this.gameSetting = new GameSetting();
            this.settingRepo = RepositoryFactory.GetGameSettingRepository(RepositoryTypes.Database);
            this.mapRepo = RepositoryFactory.GetMapRepository(RepositoryTypes.Database);
            this.switchRepo = RepositoryFactory.GetSystemSwitchRepository(RepositoryTypes.Database);

            this.Brackets = new ObservableCollection<BracketTypes>();
            this.Stages = new ObservableCollection<string>();
            this.SortOrder = new ObservableCollection<int>();
            this.Maps = new ObservableCollection<Map>();
            this.Modes = new ObservableCollection<GameModes>();

            this.LoadMaps();
            this.SelectedMap = this.Maps.First();
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

        private void LoadBrackets()
        {
            this.Brackets = new ObservableCollection<BracketTypes>()
            {
                BracketTypes.none,
                BracketTypes.swiss,
                BracketTypes.knockout
            };
        }

        private void LoadGameModes()
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

        private void LoadStages(BracketTypes bracket)
        {
            var currentStages = this.allSwitches
                .Where(x => x.Name == $"{bracket.GetDescription().ToUpper()}_STAGE")
                .Select(y => y.Value);

            this.Stages = new ObservableCollection<string>();
            foreach (var stage in currentStages)
            {
                this.Stages.Add(stage);
            }

            this.SortOrder = new ObservableCollection<int>();
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
            if (this.selectedBracket != BracketTypes.swiss && this.selectedBracket != BracketTypes.knockout)
            {
                this.DisplayLabelAsync("Cannot save with no bracket type", 2, true);
                return;
            }

            if (!this.Stages.Contains(this.SelectedStage))
            {
                this.DisplayLabelAsync("Cannot save with no stage", 2, true);
                return;
            }

            if (this.SelectedSortOrder == null || !this.SortOrder.Contains((int)this.SelectedSortOrder))
            {
                this.DisplayLabelAsync("Cannot save with no sort order", 2, true);
                return;
            }

            if (this.SelectedMap.Id <= 0)
            {
                this.DisplayLabelAsync("Cannot save with no map", 2, true);
                return;
            }

            if (this.SelectedMode == GameModes.Undefined)
            {
                this.DisplayLabelAsync("Cannot save with no mode", 2, true);
                return;
            }

            if (this.gameSetting.Id <= 0)
            {
                // Create new setting.
                try
                {
                    await Task.Run(() => this.settingRepo.AddItem(this.gameSetting));
                    this.DisplayLabelAsync("Setting saved successfully", 2);
                }
                catch
                {
                    this.DisplayLabelAsync("There was an issue while trying to save", 2, true);
                    return;
                }
            }
            else
            {
                // Update setting.
                try
                {
                    await Task.Run(() => this.settingRepo.UpdateItem(this.gameSetting));
                    this.DisplayLabelAsync("Setting updated successfully", 2);
                }
                catch
                {
                    this.DisplayLabelAsync("There was an issue while trying to update", 2, true);
                    return;
                }
            }

            this.ReloadAsync();
        }

        private async void ReloadAsync()
        {
            this.SelectedMap = this.Maps.ElementAt(0);
            this.SelectedMode = this.Modes.ElementAt(0);
            this.Brackets = new ObservableCollection<BracketTypes>();
            this.Stages = new ObservableCollection<string>();
            this.SortOrder = new ObservableCollection<int>();

            this.LoadSwitches();
            this.LoadBrackets();
            this.LoadSettings();
            this.SelectedBracket = BracketTypes.none;
        }

        private async void DeleteAsync()
        {
            // Checks if the game setting exists or not.
            if ((this.SelectedBracket != BracketTypes.knockout && this.SelectedBracket != BracketTypes.swiss)
                || !this.Stages.Contains(this.SelectedStage)
                || this.SelectedSortOrder == null 
                || !this.SortOrder.Contains((int)this.SelectedSortOrder)
                || this.gameSetting.Id <= 0)
            {
                this.DisplayLabelAsync("Cannot delete a setting that doesn't exist", 2, true);
                return;
            }

            if (MessageBox.Show($"Permenantly Delete setting: {this.gameSetting}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (await Task.Run(() => this.settingRepo.DeleteItem(this.gameSetting)))
                {
                    this.DisplayLabelAsync("Setting deleted successfully", 2);
                }
                else
                {
                    this.DisplayLabelAsync("There was an issue trying to delete", 2, true);
                    return;
                }

                this.ReloadAsync();
            }
        }
        #endregion

        #region Bindables
        public ObservableCollection<BracketTypes> Brackets
        {
            get => this.brackets;
            set => SetProperty(ref this.brackets, value);
        }

        public BracketTypes SelectedBracket
        {
            get => this.selectedBracket;
            set 
            {
                SetProperty(ref this.selectedBracket, value);
                this.LoadStages(value);
            }
        }

        public ObservableCollection<string> Stages
        {
            get => this.stages;
            set => SetProperty(ref this.stages, value);
        }

        public string SelectedStage
        {
            get
            {
                if (this.gameSetting.BracketStage == null)
                {
                    return "No Stage";
                }

                if (this.Stages.Where(x => x.ToUpper() == this.gameSetting.BracketStage.ToUpper()).Any())
                {
                    return this.Stages
                        .Where(x => x.ToUpper() == this.gameSetting.BracketStage.ToUpper())
                        .First();
                }

                return "No Stage";
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                SetProperty(ref this.gameSetting.BracketStage, value);

                if (this.allSwitches.Where(x => x.Name == $"{this.SelectedBracket.GetDescription().ToUpper()}_STAGE_{this.gameSetting.BracketStage.ToUpper()}_BO").Any())
                {
                    var val = this.allSwitches
                        .Where(x => x.Name == $"{this.selectedBracket.GetDescription().ToUpper()}_STAGE_{this.gameSetting.BracketStage.ToUpper()}_BO")
                        .First()
                        .Value;

                    var bestOf = int.TryParse(val, out int parsed) ? parsed : 0;

                    this.SortOrder = new ObservableCollection<int>();
                    if (bestOf <= 0)
                    {
                        this.SortOrder.Add(0);
                    }
                    
                    for (int i = 1; i <= bestOf; ++i)
                    {
                        this.SortOrder.Add(i);
                    }
                }
                else
                {
                    this.SortOrder = new ObservableCollection<int>() { 0 };
                }
            }
        }

        public ObservableCollection<int> SortOrder
        {
            get => this.sortOrder;
            set => SetProperty(ref this.sortOrder, value);
        }

        public int? SelectedSortOrder
        {
            get
            {
                if (this.SortOrder.Where(x => x == gameSetting.SortOrder).Any())
                {
                    return this.SortOrder.Where(x => x == gameSetting.SortOrder).First();
                }

                return null;
            }
            set
            {
                if (value == null || value == int.MinValue)
                {
                    return;
                }

                SetProperty(ref this.gameSetting.SortOrder, (int)value);

                if (this.allSettings.Where(x => 
                    x.BracketStage.ToUpper() == this.gameSetting.BracketStage.ToUpper()
                    && x.SortOrder == this.gameSetting.SortOrder).Any())
                {
                    var setting = this.allSettings
                        .Where(x => x.BracketStage.ToUpper() == this.gameSetting.BracketStage.ToUpper()
                            && x.SortOrder == this.gameSetting.SortOrder)
                        .First();

                    this.gameSetting.Id = setting.Id;
                    this.SelectedMap = this.Maps
                        .Where(x => x.Id == setting.MapId)
                        .DefaultIfEmpty(this.Maps.First())
                        .First();
                    this.SelectedMode = setting.Mode;
                }
                else
                {
                    this.SelectedMap = this.Maps.First();
                    this.SelectedMode = GameModes.Undefined;
                }
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
            set
            {
                this.gameSetting.Mode = value;
                SetProperty(ref this.selectedMode, value);
            }
        }

        public ObservableCollection<Map> Maps
        {
            get => this.maps;
            set => SetProperty(ref this.maps, value);
        }

        public Map SelectedMap
        {
            get => this.selectedMap;
            set
            {
                if (value == null)
                {
                    return;
                }

                this.gameSetting.MapId = value.Id;
                SetProperty(ref this.selectedMap, value);
            }
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
