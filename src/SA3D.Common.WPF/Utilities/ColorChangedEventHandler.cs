using System.Windows.Media;

namespace SA3D.Common.WPF.Utilities
{
	/// <summary>
	/// Event delegate for the picked color of a ColorPickerControl changing.
	/// </summary>
	/// <param name="picker">The ColorPickerControl invoking the event.</param>
	/// <param name="oldColor">Color prior to changing.</param>
	/// <param name="newColor">Color that was changed to.</param>
	public delegate void ColorChangedEventHandler(ColorPickerControl picker, Color oldColor, Color newColor);
}
