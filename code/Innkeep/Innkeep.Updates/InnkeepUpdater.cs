using System.Windows;
using Velopack;

namespace Innkeep.Updates;

public static class InnkeepUpdater
{
	public static async Task CheckForUpdates(string uri)
	{
		var manager = new UpdateManager(uri);

		var newVersionAvailable = await manager.CheckForUpdatesAsync();

		if (newVersionAvailable is null)
			return;

		var result = MessageBox.Show(
			$"There is an update available. Do you want to install the latest version? ({newVersionAvailable.TargetFullRelease.Version})",
			"Update Available",
			MessageBoxButton.YesNo
		);

		if (result == MessageBoxResult.No)
			return;

		await manager.DownloadUpdatesAsync(newVersionAvailable);

		manager.ApplyUpdatesAndRestart(newVersionAvailable);
	}
}