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
            saveCommand = new DelegateCommand(SaveAsync, () => true);
            reloadCommand = new DelegateCommand(ReloadAsync, () => true);    

            this.weaponRepo = RepositoryFactory.GetWeaponRepository("SQL");
            this.subRepo = RepositoryFactory.GetSubRepository("SQL");
            this.specialRepo = RepositoryFactory.GetSpecialRepository("SQL");

            this.lblColour = "green";
            this.model = new Weapon() { Id = -1, Name = "No Weapon Selected" };
            this.modelSpecial = new Special() { Id = -1, Name = "No Sub" };
            this.modelSub = new Sub() { Id = -1, Name = "No Sub"};

            this.LoadWeaponTypesAsync();
            this.LoadWeaponRolesAsync();
            this.LoadWeaponSubAsync();
            this.LoadWeaponSpecialAsync();
            this.LoadWeaponDataAsync();
        }
        #endregion

        #region Public methods
        public async void LoadWeaponDataAsync()
        {
            this.weapons = new ObservableCollection<Weapon>();

            var data = await Task.Run(() => this.weaponRepo.GetItems());
            foreach (var item in data)
            {
                this.weapons.Add(item);
            }
        }

        public async void LoadWeaponTypesAsync()
        {
            this.types = new ObservableCollection<WeaponType>();
            this.types.Add(WeaponType.Blaster);
            this.types.Add(WeaponType.Brella);
            this.types.Add(WeaponType.Brush);
            this.types.Add(WeaponType.Charger);
            this.types.Add(WeaponType.Dualie);
            this.types.Add(WeaponType.Roller);
            this.types.Add(WeaponType.Shooter);
            this.types.Add(WeaponType.Slosher);
            this.types.Add(WeaponType.Splatling);
        }

        public async void LoadWeaponRolesAsync()
        {
            this.roles = new ObservableCollection<WeaponRole>();
            this.roles.Add(WeaponRole.Anchor);
            this.roles.Add(WeaponRole.Frontline);
            this.roles.Add(WeaponRole.Midline);
            this.roles.Add(WeaponRole.Support);
        }

        public async void LoadWeaponSubAsync()
        {
            this.subs = new ObservableCollection<Sub>();

            var data = await Task.Run(() => this.subRepo.GetItems());
            foreach (var item in data)
            {
                this.subs.Add(item);
            }
        }

        public async void LoadWeaponSpecialAsync()
        {
            this.specials = new ObservableCollection<Special>();

            var data = await Task.Run(() => this.specialRepo.GetItems());
            foreach (var item in data)
            {
                this.specials.Add(item);
            }
        }
        #endregion

        #region Delegates
        public DelegateCommand saveCommand { get; } 

        public DelegateCommand reloadCommand { get; }
        #endregion

        #region Delegate Commands
        private async void SaveAsync()
        {
            if (this.model == null || this.modelSpecial == null || this.modelSub == null)
            {
                return;
            }

            if (this.model.Id <= 0 || this.modelSpecial.Id <= 0 || this.modelSub.Id <= 0)
            {
                return;
            }

            if (await Task.Run(() => this.weaponRepo.UpdateItem(this.model)))
            {
                this.DisplayLabelAsync($"{this.model.Name} updated successfully", 2);
                this.ReloadAsync();
            }
            else
            {
                this.DisplayLabelAsync($"There was an issue updating {this.model.Name}", 2, true);
            }
        } 

        private async void ReloadAsync()
        {
            this.LoadWeaponSubAsync();
            this.LoadWeaponSpecialAsync();
            this.LoadWeaponDataAsync();
        }
        #endregion

        #region Bindables
        public ObservableCollection<Weapon> weapons
        {
            get => this.allWeapons;
            set => SetProperty(ref this.allWeapons, value);
        }

        public Weapon selectedWeapon
        {
            get => this.model;
            set
            {
                if (value == null)
                {
                    this.model.Id = -1;
                    this.name = string.Empty;
                    this.path = string.Empty;
                    this.selectedSub = null;
                    this.selectedSpecial = null;
                    this.selectedType = WeaponType.Blaster;
                    this.selectedRole = WeaponRole.Anchor;
                }
                else
                {
                    this.model.Id = value.Id;
                    this.name = value.Name;
                    this.path = value.PicturePath;
                    this.selectedSub = this.subs.Where(x => x.Id == value.SubId).FirstOrDefault();
                    this.selectedSpecial = this.specials.Where(x => x.Id == value.SpecialId).FirstOrDefault();
                    this.selectedType = value.Type;
                    this.selectedRole = value.Role;
                }
            }
        }

        public string name
        {
            get => this.model.Name;
            set => SetProperty(ref this.model.Name, value);
        }

        public string path
        {
            get => this.model.PicturePath;
            set => SetProperty(ref this.model.PicturePath, value);
        }

        public ObservableCollection<Sub> subs
        {
            get => this.allSubs;
            set => SetProperty(ref this.allSubs, value);
        }

        public Sub selectedSub
        {
            get => this.modelSub;
            set
            {
                if (value == null)
                {
                    SetProperty(ref this.modelSub, new Sub() { Id = -1, Name = string.Empty }); 
                }
                else
                {
                    this.model.SubId = value.Id;
                    SetProperty(ref this.modelSub, value);
                }
            }
        }

        public ObservableCollection<Special> specials
        {
            get => this.allSpecials;
            set => SetProperty(ref this.allSpecials, value);
        }

        public Special selectedSpecial
        {
            get => this.modelSpecial;
            set
            {
                if (value == null)
                {
                    SetProperty(ref this.modelSpecial, new Special() { Id = -1, Name = string.Empty });
                }
                else
                {
                    this.model.SpecialId = value.Id;
                    SetProperty(ref this.modelSpecial, value);
                }
            }
        }

        public ObservableCollection<WeaponType> types
        {
            get => this.allTypes;
            set => SetProperty(ref this.allTypes, value);
        }

        public WeaponType selectedType
        {
            get => this.modelType;
            set => SetProperty(ref this.modelType, value);
        }

        public ObservableCollection<WeaponRole> roles
        {
            get => this.allRoles;
            set => SetProperty(ref this.allRoles, value);
        }

        public WeaponRole selectedRole
        {
            get => this.modelRole;
            set => SetProperty(ref this.modelRole, value);
        }

        public string lblText
        {
            get => this.labelText;
            set => SetProperty(ref this.labelText, value);
        }

        public string lblColour
        {
            get => this.labelColour;
            set => SetProperty(ref this.labelColour, value);
        }
        #endregion

        async void DisplayLabelAsync(string message, int timeDelay, bool IsError = false)
        {
            this.lblColour = (IsError) ? "red" : "green";
            this.lblText = message;
            await Task.Run(() => Thread.Sleep(timeDelay * 1000));
            this.lblText = string.Empty;
        }
    }
}
