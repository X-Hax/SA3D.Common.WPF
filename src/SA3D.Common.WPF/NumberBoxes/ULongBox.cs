using Gu.Wpf.NumericInput;
using System.Globalization;
using System;

namespace SA3D.Common.WPF.NumberBoxes
{
	/// <summary>
	/// Unsigned long box
	/// </summary>
	public class ULongBox : NumericBox<ulong>
	{
		/// <inheritdoc/>
		public override bool TryParse(string text, NumberStyles numberStyles, IFormatProvider culture, out ulong result)
		{
			return ulong.TryParse(text, numberStyles, culture, out result);
		}

		/// <inheritdoc/>
		protected override ulong Add(ulong x, ulong y)
		{
			return x + y;
		}

		/// <inheritdoc/>
		protected override ulong Subtract(ulong x, ulong y)
		{
			return x - y;
		}

		/// <inheritdoc/>
		protected override ulong TypeMax()
		{
			return ulong.MaxValue;
		}

		/// <inheritdoc/>
		protected override ulong TypeMin()
		{
			return ulong.MinValue;
		}
	}
}
