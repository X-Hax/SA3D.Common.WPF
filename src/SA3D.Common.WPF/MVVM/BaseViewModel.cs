using System.ComponentModel;

namespace SA3D.Common.WPF.MVVM
{
	/// <summary>
	/// Base viewmodel class.
	/// </summary>
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		/// <inheritdoc/>
		public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };

		/// <summary>
		/// Invokes the property changed event for a specific property.
		/// </summary>
		/// <param name="propertyName"></param>
		protected void OnPropertyChanged(string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
