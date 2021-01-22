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
using System.Windows;

namespace SquidLeagueAdmin.UI.ViewModels.Players
{
    public class PlayerViewModel : BindableBase
    {
        #region Constructor and Private Variables
        private Player Model = new Player();
        private int PlayerIndex;
        private IRepository<Player> PlayerRepo;
        private IRepository<Team> TeamRepo;
        private ObservableCollection<Player> Players;
        private ObservableCollection<Ranks> SzRank;
        private ObservableCollection<Ranks> RmRank;
        private ObservableCollection<Ranks> TcRank;
        private ObservableCollection<Ranks> CbRank;
        private ObservableCollection<Team> Teams;
        private Team TeamModel = new Team();
        private string LabelText;
        private string Colour;

        public PlayerViewModel()
        {
            SaveCommand = new DelegateCommand(this.SaveAsync, () => true);
            ReloadCommand = new DelegateCommand(this.ReloadAsync, () => true);
            DeleteCommand = new DelegateCommand(this.DeleteAsync, () => true);

            this.LoadRanks(ref this.SzRank);
            this.LoadRanks(ref this.RmRank);
            this.LoadRanks(ref this.TcRank);
            this.LoadRanks(ref this.CbRank);

            this.PlayerRepo = RepositoryFactory.GetPlayerRepository("SQL");
            this.TeamRepo = RepositoryFactory.GetTeamRepository("SQL");
            this.LoadTeamDataAsync();
            this.LoadPlayerDataAsync();
            this.LabelColour = "green";
        }
        #endregion

        #region Methods
        public void LoadRanks(ref ObservableCollection<Ranks> ranks)
        {
            ranks = new ObservableCollection<Ranks>();
            ranks.Add(Ranks.unknown);
            ranks.Add(Ranks.unranked);
            ranks.Add(Ranks.cminus);
            ranks.Add(Ranks.c);
            ranks.Add(Ranks.cplus);
            ranks.Add(Ranks.bminus);
            ranks.Add(Ranks.b);
            ranks.Add(Ranks.bplus);
            ranks.Add(Ranks.aminus);
            ranks.Add(Ranks.a);
            ranks.Add(Ranks.aplus);
            ranks.Add(Ranks.s);
            ranks.Add(Ranks.splus);
            ranks.Add(Ranks.x);
        }

        public async void LoadPlayerDataAsync()
        {
            this.players = new ObservableCollection<Player>();
            this.players.Add(new Player()
            {
                Id = -1,
                InGameName = "New Player",
                SplatZonesRank = Ranks.unknown,
                RainMakerRank = Ranks.unknown,
                TowerControlRank = Ranks.unknown,
                ClamBlitzRank = Ranks.unknown,
                TeamId = -1,
                IsActive = 0
            });

            this.selectedPlayerIndex = 0;
            var items = await Task.Run(() => this.PlayerRepo.GetItems());
            foreach (var player in items)
            {
                this.players.Add(player);
            }
        }

        public async void LoadTeamDataAsync()
        {
            this.teams = new ObservableCollection<Team>();
            this.teams.Add(new Team()
            {
                Id = -1,
                TeamName = "No Team",
                IsActive = 0
            });

            var teamItems = await Task.Run(() => this.TeamRepo.GetItems());
            foreach (var team in teamItems)
            {
                this.teams.Add(team);
            }
        }
        #endregion

        #region Delegates
        public DelegateCommand SaveCommand { get; }

        public DelegateCommand ReloadCommand { get; }

        public DelegateCommand DeleteCommand { get; }
        #endregion

        #region Deleagate Methods
        public async void SaveAsync()
        {
            if (this.Model == null || this.TeamModel == null)
            {
                return;
            }

            if (this.Model.InGameName == null || this.Model.InGameName.Trim() == string.Empty)
            {
                this.DisplayLabelAsync("Player must have a name", 2, true);
                return;
            }

            if (this.TeamModel.Id == 0)
            {
                this.TeamModel.Id = -1;
            }

            this.Model.TeamId = this.TeamModel.Id;
            if (this.Model.Id == null || this.Model.Id < 0)
            {
                try
                {
                    await Task.Run(() => this.PlayerRepo.AddItem(this.Model));
                    this.DisplayLabelAsync($"{this.Model.InGameName} Created Successfully", 2);
                }
                catch
                {
                    this.DisplayLabelAsync($"There was an issue creating {this.Model}", 2, true);
                    return;
                }
            }
            else
            {
                try
                {
                    await Task.Run(() => this.PlayerRepo.UpdateItem(this.Model));
                    this.DisplayLabelAsync($"Updated {this.Model.InGameName} Successfully", 2);
                }
                catch
                {
                    this.DisplayLabelAsync($"There was an issue while trying to update {this.Model.InGameName}", 4, true);
                    return;
                }
            }

            this.ReloadAsync();
        }

        public async void ReloadAsync()
        {
            this.LoadTeamDataAsync();
            this.LoadPlayerDataAsync();
        }

        public async void DeleteAsync()
        {
            if (this.Model == null)
            {
                return;
            }

            if (this.Model.Id < 0)
            {
                this.DisplayLabelAsync("Cannot delete a player that doesn't exist!", 3, true);
                return;
            }

            if (MessageBox.Show($"Permenantly Delete {this.Model.InGameName}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (await Task.Run(() => this.PlayerRepo.DeleteItem(this.Model)))
                {
                    this.DisplayLabelAsync($"Player {this.Model.InGameName} Deleted Successfully", 2);
                    this.ReloadAsync();
                }
                else
                {
                    this.DisplayLabelAsync($"Cannot delete player {this.Model.InGameName}", 2, true);
                }
            }
        }
        #endregion

        #region Public Variables
        public ObservableCollection<Player> players
        {
            get => this.Players;
            set => SetProperty(ref this.Players, value);
        }

        public Player selectedPlayer
        {
            get => this.Model;
            set
            {
                if (value == null)
                {
                    this.Model.Id = -1;
                    this.playerName = string.Empty;
                    this.selectedSplatZones = Ranks.unknown;
                    this.selectedRainMaker = Ranks.unknown;
                    this.selectedTowerControl = Ranks.unknown;
                    this.selectedClamBlitz = Ranks.unknown;
                    this.selectedTeam = this.Teams.ElementAt(0);
                    this.IsActive = false;
                }
                else
                {
                    this.Model.Id = value.Id;
                    this.playerName = value.InGameName;
                    this.selectedSplatZones = value.SplatZonesRank;
                    this.selectedRainMaker = value.RainMakerRank;
                    this.selectedTowerControl = value.TowerControlRank;
                    this.selectedClamBlitz = value.ClamBlitzRank;
                    var team = this.Teams.Where(x => x.Id == value.TeamId).FirstOrDefault();
                    if (team == null)
                    {
                        team = this.Teams.ElementAt(0);
                    }

                    this.selectedTeam = team;
                    this.IsActive = (value.IsActive == 1) ? true : false;
                }
            }
        }

        public int selectedPlayerIndex
        {
            get => this.PlayerIndex;
            set
            {
                if (value < 0 || value > (this.players.Count() + 1))
                {
                    SetProperty(ref this.PlayerIndex, 0);
                }
                else
                {
                    SetProperty(ref this.PlayerIndex, value);
                }

            }
        }

        public ObservableCollection<Ranks> splatZones
        {
            get => this.SzRank;
            set => SetProperty(ref this.SzRank, value);
        }

        public Ranks selectedSplatZones
        {
            get => this.Model.SplatZonesRank;
            set => SetProperty(ref this.Model.SplatZonesRank, value);
        }

        public ObservableCollection<Ranks> rainMaker
        {
            get => this.RmRank;
            set => SetProperty(ref this.RmRank, value);
        }

        public Ranks selectedRainMaker
        {
            get => this.Model.RainMakerRank;
            set => SetProperty(ref this.Model.RainMakerRank, value);
        }

        public ObservableCollection<Ranks> towerControl
        {
            get => this.TcRank;
            set => SetProperty(ref this.TcRank, value);
        }

        public Ranks selectedTowerControl
        {
            get => this.Model.TowerControlRank;
            set => SetProperty(ref this.Model.TowerControlRank, value);
        }

        public ObservableCollection<Ranks> clamBlitz
        {
            get => this.CbRank;
            set => SetProperty(ref this.CbRank, value);
        }

        public Ranks selectedClamBlitz
        {
            get => this.Model.ClamBlitzRank;
            set => SetProperty(ref this.Model.ClamBlitzRank, value);
        }

        public ObservableCollection<Team> teams
        {
            get => this.Teams;
            set => SetProperty(ref this.Teams, value);
        }

        public Team selectedTeam
        {
            get => this.TeamModel;
            set
            {
                var item = new Team();
                if (value == null)
                {
                    item.Id = -1;
                    item.TeamName = "No Team";
                    item.IsActive = 0;
                }
                else
                {
                    item = value;
                }

                SetProperty(ref this.TeamModel, value);
            }
        }

        public string playerName
        {
            get => this.Model.InGameName;
            set => SetProperty(ref this.Model.InGameName, value);
        }

        public bool IsActive
        {
            get => (this.Model.IsActive == 1) ? true : false;
            set => SetProperty(ref this.Model.IsActive, (value == true) ? 1 : 0);
        }

        public string Label
        {
            get => this.LabelText;
            set => SetProperty(ref this.LabelText, value);
        }

        public string LabelColour
        {
            get => this.Colour;
            set => SetProperty(ref this.Colour, value);
        }
        #endregion

        async void DisplayLabelAsync(string message, int timeDelay, bool IsError = false)
        {
            this.LabelColour = (IsError) ? "red" : "green";
            this.Label = message;
            await Task.Run(() => Thread.Sleep(timeDelay * 1000));
            this.Label = string.Empty;
        }
    }
}
