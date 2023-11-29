using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace SA3D.Common.WPF.ErrorHandling
{
	/// <summary>
	/// Dialog for displaying error information.
	/// </summary>
	public partial class ErrorDialog : Window
	{
		private readonly string _reportWebLink;

		/// <summary>
		/// Creates a new error dialog.
		/// </summary>
		/// <param name="programName">Name of the executing program (fallback if <see cref="Assembly.Location"/> is empty)</param>
		/// <param name="errorDescription">Description of the error.</param>
		/// <param name="log">Error log.</param>
		/// <param name="reportWebLink">Link to the website where the error should can reported.</param>
		public ErrorDialog(string programName, string errorDescription, string log, string reportWebLink)
		{
			_reportWebLink = reportWebLink;

			InitializeComponent();

			// getting the build date
			Assembly asm = Assembly.GetExecutingAssembly();
			string executable = asm.Location;
			if(string.IsNullOrWhiteSpace(asm.Location))
			{
				executable = $"{programName}.exe";
			}

			string executablePath = Path.Combine(AppContext.BaseDirectory, executable);

			string date = "--/--/---- --:--:--";
			if(File.Exists(executablePath))
			{
				date = File.GetLastWriteTimeUtc(executablePath).ToString(CultureInfo.InvariantCulture);
			}

			Title = $"{programName} Error";
			Description.Text = errorDescription;
			Log.Text = $"Program {programName}\n Build Date: {date}\n OS Version: {Environment.OSVersion}\n Log:\n{log}";
		}

		private static void UpdateClipboard(object? info)
		{
			Clipboard.SetText(info?.ToString() ?? "null");
		}

		private void Report_Click(object sender, RoutedEventArgs e)
		{
			Thread newThread = new(new ParameterizedThreadStart(UpdateClipboard));
			newThread.SetApartmentState(ApartmentState.STA);
			newThread.Start(Log.Text);

			ProcessStartInfo ps = new(_reportWebLink)
			{
				UseShellExecute = true,
				Verb = "open"
			};
			Process.Start(ps);

			DialogResult = false;
			Close();
		}

		private void Continue_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}

		private void Quit_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}
