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

namespace SquidLeagueAdmin.UI.ViewModels.Matches
{
    public class MatchViewmodel : BindableBase
    {
        #region Private variables and constructor
        private List<Match> allMatches;
        private Match currentMatch;

        private IRepository<Match> matchRepo;
        private IRepository<Caster> casterRepo;
        private IRepository<Team> teamRepo;
        private IRepository<Switch> switchRepo;

        private ObservableCollection<BracketTypes> brackets;
        private BracketTypes selectedBracket;

        private ObservableCollection<string> stages;
        private string selectedStage;

        private ObservableCollection<Match> matches;
        private Match selectedMatch;

        private ObservableCollection<Team> teams;
        private Team selectedHomeTeam;
        private Team selectedAwayTeam;

        private ObservableCollection<Caster> casters;
        private Caster selectedCaster;
        private Caster selectedSecondaryCaster;

        private string vodLink;

        private DateTime? matchDate;

        private string labelText;
        private string labelColour;

        public MatchViewmodel()
        {
            SaveCommand = new DelegateCommand(SaveAsync,() => true);
            ReloadCommand = new DelegateCommand(ReloadAsync, () => true);
            DeleteCommand = new DelegateCommand(DeleteAsync, () => true);

            this.matchRepo = RepositoryFactory.GetMatchRepository(RepositoryTypes.Database);
            this.teamRepo = RepositoryFactory.GetTeamRepository(RepositoryTypes.Database);
            this.casterRepo = RepositoryFactory.GetCasterRepository(RepositoryTypes.Database);
            this.switchRepo = RepositoryFactory.GetSystemSwitchRepository(RepositoryTypes.Database);

            this.Brackets = new ObservableCollection<BracketTypes>();
            this.Stages = new ObservableCollection<string>();
            this.Matches = new ObservableCollection<Match>();
            this.Casters = new ObservableCollection<Caster>();
            this.Teams = new ObservableCollection<Team>();

            this.LabelText = string.Empty;
            this.LabelColour = "green";

            this.LoadTeams();
            this.LoadCasters();
            this.LoadBrackets();
            this.LoadMatches();

            currentMatch = new Match() { Id = -1 };
        }
        #endregion

        #region Methods
        private void LoadBrackets()
        {
            this.Brackets = new ObservableCollection<BracketTypes>();
            this.Brackets.Add(BracketTypes.none);
            this.Brackets.Add(BracketTypes.swiss);
            this.Brackets.Add(BracketTypes.knockout);
        }

        private async void LoadMatches()
        {
            var dbMatches = await Task.Run(() => this.matchRepo.GetItems());
            this.allMatches = dbMatches.ToList();
        }

        private async void LoadCasters()
        {
            this.Casters = new ObservableCollection<Caster>()
            {
                new Caster() { Id = -1 }
            };

            var dbCasters = await Task.Run(() => this.casterRepo.GetItems());
            foreach (var caster in dbCasters)
            {
                this.Casters.Add(caster);
            }
        }

        private async void LoadTeams()
        {
            this.Teams = new ObservableCollection<Team>()
            {
                new Team() { Id = -1 }
            };

            var dbTeams = await Task.Run(() => this.teamRepo.GetItems());
            foreach (var team in dbTeams)
            {
                this.Teams.Add(team);
            }
        }
        #endregion

        #region Delegates
        public DelegateCommand SaveCommand { get; }

        public DelegateCommand ReloadCommand { get; }

        public DelegateCommand DeleteCommand { get; }
        #endregion

        #region Delegate commands
        async void SaveAsync()
        {
            throw new NotImplementedException();
        }

        async void ReloadAsync()
        {
            throw new NotImplementedException();
        }

        async void DeleteAsync()
        {
            throw new NotImplementedException();
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
            set => SetProperty(ref this.selectedBracket, value);
        }

        public ObservableCollection<string> Stages
        {
            get => this.stages;
            set => SetProperty(ref this.stages, value);
        }

        public string SelectedStage
        {
            get => this.selectedStage;
            set => SetProperty(ref this.selectedStage, value);
        }

        public ObservableCollection<Match> Matches
        {
            get => this.matches;
            set => SetProperty(ref this.matches, value);
        }

        public Match SelectedMatch
        {
            get => this.selectedMatch;
            set 
            {
                SetProperty(ref this.selectedMatch, value);
                this.currentMatch = value;
            }
        }

        public ObservableCollection<Team> Teams
        {
            get => this.teams;
            set => SetProperty(ref this.teams, value);
        }

        public Team SelectedHomeTeam
        {
            get => this.selectedHomeTeam;
            set
            {
                SetProperty(ref this.selectedHomeTeam, value);
                if (value != null && value.Id > 0)
                {
                    this.currentMatch.HomeTeamId = value.Id;
                }
            }
        }

        public Team SelectedAwayTeam
        {
            get => this.selectedAwayTeam;
            set 
            {
                SetProperty(ref this.selectedAwayTeam, value);
                if (value != null && value.Id > 0)
                {
                    this.currentMatch.AwayTeamId = value.Id;
                }
            }
        }

        public ObservableCollection<Caster> Casters
        {
            get => this.casters;
            set => SetProperty(ref this.casters, value);
        }

        public Caster SelectedCaster
        {
            get => this.selectedCaster;
            set
            {
                SetProperty(ref this.selectedCaster, value);
                if (value != null)
                {
                    this.currentMatch.CasterId = value.Id;
                }
            }
        }

        public Caster SelectedSecondCaster
        {
            get => this.selectedSecondaryCaster;
            set
            {
                SetProperty(ref this.selectedSecondaryCaster, value);
                if (value != null)
                {
                    this.currentMatch.SecondaryCasterId = value.Id;
                }
            }
        }

        public string VodLink
        {
            get => this.vodLink;
            set
            {
                SetProperty(ref this.vodLink, value);
                this.currentMatch.MatchVod = value;
            }
        }

        public DateTime? MatchDate
        {
            get => this.matchDate;
            set
            {
                SetProperty(ref this.matchDate, value);
                if (value != null)
                {
                    value = new DateTime(value.Value.Hour, value.Value.Minute, 0).ToUniversalTime();
                }

                this.currentMatch.MatchDate = value;
            }
        }

        public string LabelText
        {
            get => this.labelText;
            set => SetProperty(ref this.labelText, value);
        }

        public string LabelColour
        {
            get => this.labelColour;
            set => SetProperty(ref this.labelColour, value);
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
