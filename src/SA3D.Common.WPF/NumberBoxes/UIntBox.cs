using Gu.Wpf.NumericInput;
using System.Globalization;
using System;

namespace SA3D.Common.WPF.NumberBoxes
{
	/// <summary>
	/// Unsigned integer box
	/// </summary>
	public class UIntBox : NumericBox<uint>
	{
		/// <inheritdoc/>
		public override bool TryParse(string text, NumberStyles numberStyles, IFormatProvider culture, out uint result)
		{
			return uint.TryParse(text, numberStyles, culture, out result);
		}

		/// <inheritdoc/>
		protected override uint Add(uint x, uint y)
		{
			return x + y;
		}

		/// <inheritdoc/>
		protected override uint Subtract(uint x, uint y)
		{
			return x - y;
		}

		/// <inheritdoc/>
		protected override uint TypeMax()
		{
			return uint.MaxValue;
		}

		/// <inheritdoc/>
		protected override uint TypeMin()
		{
			return uint.MinValue;
		}
	}
}
