using System;

namespace SA3D.Common.WPF.ErrorHandling
{
	/// <summary>
	/// Provides a method for getting the a custom description for an exception.
	/// </summary>
	public interface IErrorHandler
	{
		/// <summary>
		/// Returns the description for an error. Returns null if not handled.
		/// </summary>
		/// <param name="exception">The exception to get the description for.</param>
		/// <returns>The error description.</returns>
		public string? GetErrorDescription(Exception exception);
	}
}
