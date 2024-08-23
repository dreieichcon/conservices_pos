namespace Innkeep.Core.FileIo;

public static class PathHelper
{
	public static string GetDesktopExportPath()
	{
		var outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "InnkeepExport");

		if (!Directory.Exists(outputFolder))
			Directory.CreateDirectory(outputFolder);

		return outputFolder;
	}

	public static string GetTimestampExportPath(string path, string filename)
	{
		return Path.Combine(path, DateTime.Now.ToString("yyyyMMdd_HHmmss_") + filename);
	}
}