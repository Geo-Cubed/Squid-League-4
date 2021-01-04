using Prism.Mvvm;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.UI.ViewModels.Teams
{
    public class TeamViewModel : BindableBase
    {
        public Team Model { get; set; }

        private IRepository<Team> repo;

        public IEnumerable<Team> AllTeams;

        public TeamViewModel()
        {
            // TODO: Delegate command.

            this.repo = RepositoryFactory.GetTeamRepository("SQL");
            this.AllTeams = repo.GetItems();
        }
    }
}
