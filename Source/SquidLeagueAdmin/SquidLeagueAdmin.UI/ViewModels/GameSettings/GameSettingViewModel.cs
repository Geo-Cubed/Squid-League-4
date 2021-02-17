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
        private int bracketIndex;

        private ObservableCollection<string> stages;
        private int stageIndex;

        private ObservableCollection<int> sortOrder;
        private int sortOrderIndex;

        private ObservableCollection<Map> maps;

        private ObservableCollection<GameModes> modes;

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
            this.Brackets = new ObservableCollection<BracketTypes>()
            {
                BracketTypes.swiss,
                BracketTypes.knockout
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

                var currentStages = this.allSwitches
                    .Where(x => x.Name == $"{value.GetDescription().ToUpper()}_STAGE")
                    .Select(y => y.Value);

                this.Stages = new ObservableCollection<string>();
                foreach (var stage in currentStages)
                {
                    this.Stages.Add(stage);
                }
            }
        }

        public int BracketIndex
        {
            get => this.bracketIndex;
            set
            {
                if (value <= 0)
                {
                    SetProperty(ref this.bracketIndex, 0);
                }
                else
                {
                    SetProperty(ref this.bracketIndex, value);
                }
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

        public int StageIndex
        {
            get => this.stageIndex;
            set
            {
                if (value <= 0 || value > this.Stages.Count() - 1)
                {
                    SetProperty(ref this.stageIndex, 0);
                }
                else
                {
                    SetProperty(ref this.stageIndex, value);
                }
            }
        }

        public ObservableCollection<int> SortOrder
        {
            get => this.sortOrder;
            set => SetProperty(ref this.sortOrder, value);
        }

        public int SelectedSortOrder
        {
            get
            {
                if (this.SortOrder.Where(x => x == gameSetting.SortOrder).Any())
                {
                    return this.SortOrder.Where(x => x == gameSetting.SortOrder).First();
                }

                return 0;
            }
            set
            {
                SetProperty(ref this.gameSetting.SortOrder, value);

                if (this.allSettings.Where(x => 
                    x.BracketStage.ToUpper() == this.gameSetting.BracketStage.ToUpper()
                    && x.SortOrder == this.gameSetting.SortOrder).Any())
                {
                    var setting = this.allSettings
                        .Where(x => x.BracketStage.ToUpper() == this.gameSetting.BracketStage.ToUpper()
                            && x.SortOrder == this.gameSetting.SortOrder)
                        .First();

                    this.SelectedMap = this.Maps.Where(x => x.Id == setting.MapId).First();
                    this.SelectedMode = setting.Mode;
                }
                else
                {
                    this.SelectedMap = this.Maps.ElementAt(0);
                    this.SelectedMode = GameModes.Undefined;
                }
            }
        }

        public int SortOrderIndex
        {
            get => this.sortOrderIndex;
            set
            {
                if (value <= 0 || value > this.SortOrder.Count() - 1)
                {
                    SetProperty(ref this.sortOrderIndex, 0);
                }
                else
                {
                    SetProperty(ref this.sortOrderIndex, value);
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
            get => this.gameSetting.Mode;
            set => SetProperty(ref this.gameSetting.Mode, value);
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
