using Prism.Mvvm;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SquidLeagueAdmin.UI.ViewModels.Teams
{
    public class TeamViewModel : BindableBase
    {
        public Team Model { get; set; }

        private IRepository<Team> repo;
        private ObservableCollection<Team> AllTeams;
        private string LabelText;

        public TeamViewModel()
        {
            this.AllTeams = new ObservableCollection<Team>() { new Team() { Id = -1, TeamName = "New Team", IsActive = 0 } };
            this.repo = RepositoryFactory.GetTeamRepository("SQL");
            var items = repo.GetItems();
            foreach (var team in items)
            {
                this.AllTeams.Add(team);
            }
            
            this.Model = new Team() { TeamName = "Ducklings", IsActive = 1 };
        }

        public ObservableCollection<Team> Teams
        {
            get => this.AllTeams;
            set => SetProperty(ref this.AllTeams, value);
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
    }
}
