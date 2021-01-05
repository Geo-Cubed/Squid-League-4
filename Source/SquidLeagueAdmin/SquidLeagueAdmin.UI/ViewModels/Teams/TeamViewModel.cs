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
using System.Windows;

namespace SquidLeagueAdmin.UI.ViewModels.Teams
{
    public class TeamViewModel : BindableBase
    {
        #region Constructor and Private Variables
        private Team Model { get; set; }
        private IRepository<Team> repo;
        private ObservableCollection<Team> AllTeams;
        private string LabelText;
        private string Colour;

        public TeamViewModel()
        {
            SaveCommand = new DelegateCommand(SaveAsync, () => true);
            ReloadCommand = new DelegateCommand(LoadTeamsAsync, () => true);
            DeleteCommand = new DelegateCommand(DeleteTeamAsync, () => true);

            this.repo = RepositoryFactory.GetTeamRepository("SQL");
            this.LoadTeamsAsync();
            this.Model = new Team() { Id = -1 };
            this.LabelColour = "green";
        }
        #endregion


        #region Delegates
        public DelegateCommand SaveCommand { get; }

        public DelegateCommand ReloadCommand { get; }

        public DelegateCommand DeleteCommand { get; }
        #endregion

        #region Delegate Commands
        async void LoadTeamsAsync()
        {
            this.Teams = new ObservableCollection<Team>();
            this.Teams.Add(new Team() { Id = -1, TeamName = "New Team", IsActive = 0 });
            var items = await Task.Run(() => repo.GetItems());
            foreach (var team in items)
            {
                this.Teams.Add(team);
            }
        }

        async void SaveAsync()
        {
            // Create New Team.
            if (this.Model.Id < 0)
            {
                try
                {
                    await Task.Run(() => this.repo.AddItem(this.Model));
                    this.DisplayLabelAsync($"{this.Model.TeamName} Created Successfully", 2);
                }
                catch
                {
                    this.DisplayLabelAsync($"There was an error creating {this.Model.TeamName}", 2, true);
                    return;
                }
            }
            else // Update Team.
            {
                try
                {
                    await Task.Run(() => this.repo.UpdateItem(this.Model));
                    this.DisplayLabelAsync($"{this.Model.TeamName} Updated Successfully", 2);
                }
                catch
                {
                    this.DisplayLabelAsync($"There was an error updating {this.Model.TeamName}", 2, true);
                    return;
                }
            }

            this.LoadTeamsAsync();
        }

        async void DeleteTeamAsync()
        {
            if (SelectedTeam.Id < 0)
            {
                this.DisplayLabelAsync("Cannot delete a non existant team.", 2, true);
                return;
            }

            if (MessageBox.Show($"Permenantly Delete {SelectedTeam.TeamName}?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (await Task.Run(() => this.repo.DeleteItem(this.SelectedTeam)))
                {
                    this.DisplayLabelAsync($"Team {SelectedTeam.TeamName} Deleted Successfully", 2);
                    this.LoadTeamsAsync();
                }
                else
                {
                    this.DisplayLabelAsync($"Cannot delete team {SelectedTeam.TeamName}", 2, true);
                }
            }
        }
        #endregion

        #region Public Variables
        public ObservableCollection<Team> Teams
        {
            get => this.AllTeams;
            set => SetProperty(ref this.AllTeams, value);
        }

        public Team SelectedTeam
        {
            get => this.Model;
            set
            {
                if (value == null)
                {
                    this.TeamName = string.Empty;
                    this.IsActive = false;
                    this.Model.Id = -1;
                }
                else
                {
                    this.TeamName = value.TeamName;
                    this.IsActive = (value.IsActive == 1) ? true : false;
                    this.Model.Id = value.Id;
                }
            }
        }

        public string TeamName
        {
            get => this.Model.TeamName;
            set => SetProperty(ref this.Model.TeamName, value);
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
