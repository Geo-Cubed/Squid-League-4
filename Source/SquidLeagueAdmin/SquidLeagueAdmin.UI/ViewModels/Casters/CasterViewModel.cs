using Prism.Commands;
using Prism.Mvvm;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.UI.ViewModels.Casters
{
    public class CasterViewModel : BindableBase
    {
        private Caster Model;
        private IRepository<Caster> casterRepo;

        public CasterViewModel()
        {
            // TODO: Assign delegates.
            this.casterRepo = RepositoryFactory.GetCasterRepository("SQL");

            // TODO: Load data.
        }

        #region delegates
        public DelegateCommand saveCommand { get; }

        public DelegateCommand reloadCommand { get; }

        public DelegateCommand delegateCommand { get; }
        #endregion

        // TODO: Bindings.
    }
}
