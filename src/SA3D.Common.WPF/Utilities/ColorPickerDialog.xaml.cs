using System.Windows;
using System.Windows.Media;

namespace SA3D.Common.WPF.Utilities
{
	/// <summary>
	/// Color picker dialog.
	/// </summary>
	public partial class ColorPickerDialog : Window
	{
		private ColorPickerDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Opens the color picker as a dialog.
		/// </summary>
		/// <param name="color">The color to start with.</param>
		/// <param name="pickedColor">The output color.</param>
		/// <returns>Whether a color was picked.</returns>
		public static bool ShowAsDialog(Color color, out Color pickedColor)
		{
			ColorPickerDialog picker = new();
			picker.ColorPickerControl.Color = color;

			if(picker.ShowDialog() == true)
			{
				pickedColor = picker.ColorPickerControl.Color;
				return true;
			}
			else
			{
				pickedColor = default;
				return false;
			}
		}

		private void Submit(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}

		private void Cancel(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}

	}
}
