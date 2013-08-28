using System;
using System.Linq;
using System.Windows.Input;

namespace RosyChopper.Commands
{
	public class DelegateCommand : ICommand
	{
		private readonly Predicate<object> canExecute;
		private readonly Action<object> execute;

		public DelegateCommand(Action<object> execute) : this(execute, null)
		{
		}

		public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			if (this.canExecute == null)
			{
				return true;
			}

			return this.canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			this.execute(parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			if (this.CanExecuteChanged != null)
			{
				this.CanExecuteChanged(this, EventArgs.Empty);
			}
		}
	}
}