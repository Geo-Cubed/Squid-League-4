using Prism.Mvvm;
using SquidLeagueAdmin.Models;
using SquidLeagueAdmin.Models.Enums;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SquidLeagueAdmin.UI.ViewModels.Weapons
{
    public class WeaponViewModel : BindableBase
    {
        #region Constructor and private variables
        private Weapon model;
        private ObservableCollection<Weapon> allWeapons;

        private WeaponType modelType;
        private ObservableCollection<WeaponType> allTypes;

        private WeaponRole modelRole;
        private ObservableCollection<WeaponRole> allRoles;

        private Sub modelSub;
        private ObservableCollection<Sub> allSubs;

        private Special modelSpecial;
        private ObservableCollection<Special> allSpecials;

        private IRepository<Weapon> weaponRepo;
        private IRepository<Sub> subRepo;
        private IRepository<Special> specialRepo;

        private string labelText;
        private string labelColour;

        public WeaponViewModel()
        {
            // TODO: Delegate commands.

            this.weaponRepo = RepositoryFactory.GetWeaponRepository("SQL");
            this.subRepo = RepositoryFactory.GetSubRepository("SQL");
            this.specialRepo = RepositoryFactory.GetSpecialRepository("SQL");

            this.model = new Weapon() { Id = -1, Name = "No Weapon Selected" };
            this.modelSpecial = new Special() { Id = -1, Name = "No Sub" };
            this.modelSub = new Sub() { Id = -1, Name = "No Sub"};
        }
        #endregion

        #region Public methods
        public async void LoadWeaponDataAsync()
        {
            // TODO: Load data.
        }

        public async void LoadWeaponTypesAsync()
        {

        }

        public async void LoadWeaponRolesAsync()
        {

        }

        public async void LoadWeaponSubAsync()
        {

        }

        public async void LoadWeaponSpecialAsync()
        {

        }
        #endregion
    }
}
