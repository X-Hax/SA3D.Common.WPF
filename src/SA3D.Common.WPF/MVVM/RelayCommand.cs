using System;
using System.Windows.Input;

namespace SA3D.Common.WPF.MVVM
{
	/// <summary>
	/// A basic command that runs an Action with a specific parameter
	/// </summary>
	public class RelayCommand<TParameterType> : ICommand
	{
		#region Private Members

		/// <summary>
		/// The action to run
		/// </summary>
		private readonly Action<TParameterType> _action;

		#endregion

		#region Public Events

		/// <summary>
		/// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
		/// </summary>
		public event EventHandler? CanExecuteChanged = (sender, e) => { };

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public RelayCommand(Action<TParameterType> action)
		{
			_action = action;
		}

		#endregion

		#region Command Methods

		/// <summary>
		/// A relay command can always execute
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public bool CanExecute(object? parameter)
		{
			return true;
		}

		/// <summary>
		/// Executes the commands Action
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object? parameter)
		{
			if(parameter is TParameterType typeparam)
			{
				_action(typeparam);
			}
			else
			{
				throw new ArgumentException("Parameter of type " + parameter?.GetType() + ", but it should be " + typeof(TParameterType), nameof(parameter));
			}
		}

		#endregion
	}

	/// <summary>
	/// A basic command that runs an Action
	/// </summary>
	public class RelayCommand : ICommand
	{
		#region Private Members

		/// <summary>
		/// The action to run
		/// </summary>
		private readonly Action _action;

		#endregion

		#region Public Events

		/// <summary>
		/// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
		/// </summary>
		public event EventHandler? CanExecuteChanged = (sender, e) => { };
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="action">The action that should be performed upon being called</param>
		public RelayCommand(Action action)
		{
			_action = action;
		}

		#endregion

		#region Command Methods

		/// <summary>
		/// A relay command can always execute
		/// </summary>
		/// <param name="parameter">Input parameter (unused)</param>
		/// <returns></returns>
		public bool CanExecute(object? parameter)
		{
			return true;
		}

		/// <summary>
		/// Executes the commands Action
		/// </summary>
		/// <param name="parameter">Input parameter (unused)</param>
		public void Execute(object? parameter)
		{
			_action();
		}

		#endregion
	}
}