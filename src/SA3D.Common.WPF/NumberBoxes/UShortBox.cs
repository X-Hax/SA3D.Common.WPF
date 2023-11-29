using Gu.Wpf.NumericInput;
using System.Globalization;
using System;

namespace SA3D.Common.WPF.NumberBoxes
{
	/// <summary>
	/// Unsigned short box
	/// </summary>
	public class UShortBox : NumericBox<ushort>
	{
		/// <inheritdoc/>
		public override bool TryParse(string text, NumberStyles numberStyles, IFormatProvider culture, out ushort result)
		{
			return ushort.TryParse(text, numberStyles, culture, out result);
		}

		/// <inheritdoc/>
		protected override ushort Add(ushort x, ushort y)
		{
			return (ushort)(x + y);
		}

		/// <inheritdoc/>
		protected override ushort Subtract(ushort x, ushort y)
		{
			return (ushort)(x - y);
		}

		/// <inheritdoc/>
		protected override ushort TypeMax()
		{
			return ushort.MaxValue;
		}

		/// <inheritdoc/>
		protected override ushort TypeMin()
		{
			return ushort.MinValue;
		}
	}
}
