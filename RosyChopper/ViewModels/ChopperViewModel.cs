using System.ComponentModel;
using System.Runtime.CompilerServices;
using RosyChopper.Commands;

namespace RosyChopper.ViewModels
{
	public class ChopperViewModel : INotifyPropertyChanged
	{
		private bool isEditing;

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
					this.RaisePropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public DelegateCommand StartEditingCommand { get; private set; }
		public DelegateCommand StopEditingCommand { get; private set; }

		public ChopperViewModel()
		{
			this.StartEditingCommand = new DelegateCommand(ExecuteStartEditing, (obj) => !this.IsEditing);
			this.StopEditingCommand = new DelegateCommand(ExecuteStopEditing, (obj) => this.IsEditing);
		}

		private void ExecuteStartEditing(object parameter)
		{
			this.IsEditing = true;
		}

		private void ExecuteStopEditing(object parameter)
		{
			this.IsEditing = false;
		}

		private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			var handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}