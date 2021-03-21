using Prism.Commands;
using Prism.Mvvm;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SquidLeagueAdmin.UI.ViewModels.Matches
{
    public class MatchViewmodel : BindableBase
    {
        #region Private variables and constructor
        private List<Match> allMatches;
        private Match currentMatch;

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

            this.LabelText = string.Empty;
            this.LabelColour = "green";
        }
        #endregion

        #region Delegates
        public DelegateCommand SaveCommand;

        public DelegateCommand ReloadCommand;

        public DelegateCommand DeleteCommand;
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
            set => SetProperty(ref this.selectedMatch, value);
        }

        public ObservableCollection<Team> Teams
        {
            get => this.teams;
            set => SetProperty(ref this.teams, value);
        }

        public Team SelectedHomeTeam
        {
            get => this.selectedHomeTeam;
            set => SetProperty(ref this.selectedHomeTeam, value);
        }

        public Team SelectedAwayTeam
        {
            get => this.selectedAwayTeam;
            set => SetProperty(ref this.selectedAwayTeam, value);
        }

        public ObservableCollection<Caster> Casters
        {
            get => this.casters;
            set => SetProperty(ref this.casters, value);
        }

        public Caster SelectedCaster
        {
            get => this.selectedCaster;
            set => SetProperty(ref this.selectedCaster, value);
        }

        public Caster SelectedSecondCaster
        {
            get => this.selectedSecondaryCaster;
            set => SetProperty(ref this.selectedSecondaryCaster, value);
        }

        public string VodLink
        {
            get => this.vodLink;
            set => SetProperty(ref this.vodLink, value);
        }

        public DateTime? MatchDate
        {
            get => this.matchDate;
            set => SetProperty(ref this.matchDate, value);
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
