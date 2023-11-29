using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace SA3D.Common.WPF.ErrorHandling
{
	/// <summary>
	/// Error handler.
	/// </summary>
	public class ErrorHandler
	{
		/// <summary>
		/// Link to the website where the error should can reported.
		/// </summary>
		public string ReportWebLink { get; set; }

		/// <summary>
		/// Custom error handlers.
		/// </summary>
		public List<IErrorHandler> ErrorHandlers { get; }

		/// <summary>
		/// Creates a new error handler.
		/// </summary>
		/// <param name="reportWebLink">Link to the website where the error should can reported.</param>
		public ErrorHandler(string reportWebLink)
		{
			ReportWebLink = reportWebLink;
			ErrorHandlers = new();
		}

		/// <summary>
		/// Action to execute when the application encounters an unhandled exception.
		/// </summary>
		/// <param name="source">Object from which the exception originates.</param>
		/// <param name="eventArgs">Event arguments.</param>
		public void OnUnhandledException(object source, UnhandledExceptionEventArgs eventArgs)
		{
			HandleException((Exception)eventArgs.ExceptionObject);
		}

		/// <summary>
		/// Handles a thrown exception.
		/// </summary>
		/// <param name="exception">The thrown exception.</param>
		public void HandleException(Exception exception)
		{
			string app = Assembly.GetEntryAssembly()?.GetName().Name ?? "app";

			try
			{

				if(exception.InnerException != null && exception is XamlParseException)
				{
					exception = exception.InnerException;
				}

				string? errorDescription = null;

				foreach(IErrorHandler errorHandler in ErrorHandlers)
				{
					errorDescription = errorHandler.GetErrorDescription(exception);
					if(errorDescription != null)
					{
						break;
					}
				}

				errorDescription ??= $"{app} has crashed with the following error:\n  {exception.GetType().Name}.\n\n" +
						"If you wish to report a bug, please include the following in your report:";


				ErrorDialog dialog = new(app, errorDescription, exception.ToString(), ReportWebLink);

				if(dialog.ShowDialog() != true)
				{
					Application.Current?.Shutdown();
				}
			}
			catch(Exception ex2)
			{
				MessageBox.Show(ex2.ToString(), $"{app} Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);

				string logPath = AppContext.BaseDirectory + $"\\{app}.log";
				File.WriteAllText(logPath, exception.ToString());
				MessageBox.Show($"Unhandled Exception {exception.GetType().Name}\nLog file has been saved to:\n{logPath}", $"{app} Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
				Application.Current?.Shutdown();
			}
		}
	}
}
