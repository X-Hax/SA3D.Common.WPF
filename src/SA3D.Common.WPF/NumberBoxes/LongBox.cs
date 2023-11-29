using Gu.Wpf.NumericInput;
using System.Globalization;
using System;

namespace SA3D.Common.WPF.NumberBoxes
{
	/// <summary>
	/// Signed long box
	/// </summary>
	public class LongBox : NumericBox<long>
	{
		/// <inheritdoc/>
		public override bool TryParse(string text, NumberStyles numberStyles, IFormatProvider culture, out long result)
		{
			return long.TryParse(text, numberStyles, culture, out result);
		}

		/// <inheritdoc/>
		protected override long Add(long x, long y)
		{
			return x + y;
		}

		/// <inheritdoc/>
		protected override long Subtract(long x, long y)
		{
			return x - y;
		}

		/// <inheritdoc/>
		protected override long TypeMax()
		{
			return long.MaxValue;
		}

		/// <inheritdoc/>
		protected override long TypeMin()
		{
			return long.MinValue;
		}
	}
}
