using Prism.Commands;
using Prism.Mvvm;
using SquidLeagueAdmin.RepoFactory;
using SquidLeagueAdmin.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Switch = SquidLeagueAdmin.Models.SystemSwitch;

namespace SquidLeagueAdmin.UI.ViewModels.SystemSwitch
{
    public class SystemSwitchViewModel : BindableBase
    {
        private Switch currentSwitch;
        private int selectedSwitchIndex;
        private ObservableCollection<Switch> allSwitches;
        private IRepository<Switch> switchRepo;
        private string lblColour;
        private string lblText;

        public SystemSwitchViewModel()
        {
            SaveCommand = new DelegateCommand(SaveAsync, () => true);
            ReloadCommand = new DelegateCommand(ReloadAsync, () => true);
            DeleteCommand = new DelegateCommand(DeleteAsync, () => true);

            this.currentSwitch = new Switch();
            this.LabelColour = "green";

            this.switchRepo = RepositoryFactory.GetSystemSwitchRepository("SQL");
            this.LoadDataAsync();
        }
        
        public async void LoadDataAsync()
        {
            this.Switches = new ObservableCollection<Switch>();
            this.Switches.Add(new Switch() { Id = -1, Name = "New Switch" });

            var data = await Task.Run(() => this.switchRepo.GetItems());
            foreach (var item in data)
            {
                this.Switches.Add(item);
            }
        }

        public DelegateCommand SaveCommand { get; }

        public DelegateCommand ReloadCommand { get; }

        public DelegateCommand DeleteCommand { get; }

        public async void SaveAsync()
        {
            if (this.SelectedSwitch == null)
            {
                return;
            }

            if (this.SelectedSwitch.Name == null || this.SelectedSwitch.Name.Trim() == string.Empty)
            {
                this.DisplayLabelAsync("You cannot save a switch without a name", 2, true);
                return;
            }
            
            if (this.SelectedSwitch.Value == null || this.SelectedSwitch.Value.Trim() == string.Empty)
            {
                this.DisplayLabelAsync("You cannot save a switch without a value", 2, true);
                return;
            }

            if (this.SelectedSwitch.Id <= 0)
            {
                try
                {
                    await Task.Run(() => this.switchRepo.AddItem(this.SelectedSwitch));
                    this.DisplayLabelAsync($"{this.SelectedSwitch} Created Successfully", 2);
                }
                catch
                {
                    this.DisplayLabelAsync($"There was an issue creating {this.SelectedSwitch}", 2, true);
                    return;
                }
            }
            else
            {
                try
                {
                    await Task.Run(() => this.switchRepo.UpdateItem(this.SelectedSwitch));
                    this.DisplayLabelAsync($"{this.SelectedSwitch} Updated Successfully", 2);
                }
                catch
                {
                    this.DisplayLabelAsync($"There was an issue updating {this.SelectedSwitch}", 2, true);
                    return;
                }
            }

            this.LoadDataAsync();
        }

        public async void ReloadAsync()
        {
            this.LoadDataAsync();
        }

        public async void DeleteAsync()
        {
            if (this.SelectedSwitch == null)
            {
                return;
            }

            if (this.SelectedSwitch.Id <= 0)
            {
                this.DisplayLabelAsync("Cannot delete a switch that doesn't exist", 2, true);
                return;
            }

            if (MessageBox.Show($"Permenantly Delete {this.SelectedSwitch}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (await Task.Run(() => this.switchRepo.DeleteItem(this.SelectedSwitch)))
                {
                    this.DisplayLabelAsync($"Switch {this.SelectedSwitch} Deleted Successfully", 2);
                    this.ReloadAsync();
                }
                else
                {
                    this.DisplayLabelAsync($"Cannot delete switch {this.SelectedSwitch}", 2, true);
                }
            }
        }

        public ObservableCollection<Switch> Switches
        {
            get => this.allSwitches;
            set => SetProperty(ref this.allSwitches, value);
        }

        public Switch SelectedSwitch
        {
            get => this.currentSwitch;
            set
            {
                if (value == null)
                {
                    this.currentSwitch.Id = -1;
                    this.Name = string.Empty;
                    this.Value = string.Empty;
                }
                else
                {
                    this.currentSwitch.Id = value.Id;
                    this.Name = value.Name;
                    this.Value = value.Value;
                }
            }
        }

        public int SwitchIndex
        {
            get => this.selectedSwitchIndex;
            set
            {
                if (value < 0 || value > (this.Switches.Count + 1))
                {
                    SetProperty(ref this.selectedSwitchIndex, 0);
                }
                else
                {
                    SetProperty(ref this.selectedSwitchIndex, value);
                }
            }
        }

        public string Name
        {
            get => this.SelectedSwitch.Name;
            set
            {
                var upperValue = value.ToUpper().Replace(' ', '_');
                SetProperty(ref this.SelectedSwitch.Name, upperValue);
            }
        }

        public string Value
        {
            get => this.SelectedSwitch.Value;
            set => SetProperty(ref this.SelectedSwitch.Value, value);
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

        async void DisplayLabelAsync(string message, int timeDelay, bool IsError = false)
        {
            this.LabelColour = (IsError) ? "red" : "green";
            this.LabelText = message;
            await Task.Run(() => Thread.Sleep(timeDelay * 1000));
            this.LabelText = string.Empty;
        }
    }
}
