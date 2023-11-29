using Gu.Wpf.NumericInput;
using System;
using System.Globalization;

namespace SA3D.Common.WPF.NumberBoxes
{
	/// <summary>
	/// Byte number box.
	/// </summary>
	public class ByteBox : NumericBox<byte>
	{
		/// <inheritdoc/>
		public override bool TryParse(string text, NumberStyles numberStyles, IFormatProvider culture, out byte result)
		{
			return byte.TryParse(text, numberStyles, culture, out result);
		}

		/// <inheritdoc/>
		protected override byte Add(byte x, byte y)
		{
			return (byte)(x + y);
		}

		/// <inheritdoc/>
		protected override byte Subtract(byte x, byte y)
		{
			return (byte)(x - y);
		}

		/// <inheritdoc/>
		protected override byte TypeMax()
		{
			return byte.MaxValue;
		}

		/// <inheritdoc/>
		protected override byte TypeMin()
		{
			return byte.MinValue;
		}
	}
}
