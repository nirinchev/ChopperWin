using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RosyChopper.Commands;
using RosyChopper.Controllers;
using RosyChopper.Models;

namespace RosyChopper.ViewModels
{
	public class ChopperViewModel : INotifyPropertyChanged
	{
		private bool isEditing;

		private string newPersonName;

		public ChopperViewModel()
		{
			this.StartEditingCommand = new DelegateCommand(this.ExecuteStartEditing, (obj) => !this.IsEditing);
			this.StopEditingCommand = new DelegateCommand(this.ExecuteStopEditing, (obj) => this.IsEditing);
			this.AddPersonCommand = new DelegateCommand(this.AddPerson, this.CanAddPerson);
			this.RemovePersonCommand = new DelegateCommand(this.RemovePerson, (obj) => this.IsEditing);
			this.PickRandomPersonCommand = new DelegateCommand(this.PickRandomPerson, (obj) => !this.IsEditing);

			if (!DataController.IsDataLoaded)
			{
				DataController.DataLoaded += this.OnDataControllerDataLoaded;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public bool IsEditing
		{
			get
			{
				return this.isEditing;
			}
			set
			{
				if (value != this.isEditing)
				{
					this.isEditing = value;
					this.StartEditingCommand.RaiseCanExecuteChanged();
					this.StopEditingCommand.RaiseCanExecuteChanged();
					this.AddPersonCommand.RaiseCanExecuteChanged();
					this.RemovePersonCommand.RaiseCanExecuteChanged();
					this.PickRandomPersonCommand.RaiseCanExecuteChanged();
					this.SelectedPerson = null;
					this.RaisePropertyChanged();
				}
			}
		}

		public string NewPersonName
		{
			get
			{
				return this.newPersonName;
			}
			set
			{
				if (value != this.newPersonName)
				{
					this.newPersonName = value;
					this.AddPersonCommand.RaiseCanExecuteChanged();
					this.RaisePropertyChanged();
				}
			}
		}

		private Person selectedPerson;

		public Person SelectedPerson
		{
			get
			{
				return this.selectedPerson;
			}
			set
			{
				if (value != this.selectedPerson)
				{
					this.selectedPerson = value;
					this.RaisePropertyChanged();
				}
			}
		}

		public PeopleCollection People
		{
			get
			{
				return DataController.People;
			}
		}

		public DelegateCommand StartEditingCommand { get; private set; }

		public DelegateCommand StopEditingCommand { get; private set; }

		public DelegateCommand AddPersonCommand { get; private set; }

		public DelegateCommand RemovePersonCommand { get; private set; }

		public DelegateCommand PickRandomPersonCommand { get; private set; }

		private void OnDataControllerDataLoaded(object sender, System.EventArgs e)
		{
			this.RaisePropertyChanged("People");
		}

		private void ExecuteStartEditing(object parameter)
		{
			this.IsEditing = true;
		}

		private void ExecuteStopEditing(object parameter)
		{
			this.IsEditing = false;
		}

		private void AddPerson(object parameter)
		{
			this.People.Add(new Person(this.NewPersonName));
			this.NewPersonName = string.Empty;
		}

		private void RemovePerson(object parameter)
		{
			// TODO
			//var person = parameter as Person;
			//if (person != null && this.People.Contains(person))
			//{
			//	this.People.Remove(person);
			//}
			if (this.SelectedPerson != null && this.People.Contains(this.SelectedPerson))
			{
				this.People.Remove(this.SelectedPerson);
			}
		}

		private bool CanAddPerson(object obj)
		{
			var result = this.IsEditing &&
						 !string.IsNullOrEmpty(this.NewPersonName) &&
						 !this.People.Any(p => p.Name.Equals(this.NewPersonName, StringComparison.OrdinalIgnoreCase));

			return result;
		}

		private static readonly Random random = new Random();
		private async void PickRandomPerson(object obj)
		{
			for (var i = 0; i < 30; i++)
			{
				var selected = random.Next(this.People.Count);
				this.SelectedPerson = this.People[selected];
				await Task.Delay(TimeSpan.FromMilliseconds(50));
			}
		}

		private void RaisePropertyChanged([CallerMemberName]
										  string propertyName = "")
		{
			var handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}