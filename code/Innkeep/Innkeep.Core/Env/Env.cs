namespace Innkeep.Core.Env;

public static class Env
{
	/// <summary>
	///     Loads environment variables from a file
	/// </summary>
	/// <param name="filePath">Path to the env file.</param>
	/// <returns>True if the variables could be loaded, false if not.</returns>
	public static bool Load(string filePath)
	{
		// check if the file exists
		if (!File.Exists(filePath))
			return false;

		// try to read in all environment variables in the file
		try
		{
			foreach (var line in File.ReadAllLines(filePath))
			{
				var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

				if (parts.Length != 2)
					continue;

				Environment.SetEnvironmentVariable(parts[0], parts[1]);
			}
		}
		catch (Exception)
		{
			return false;
		}

		return true;
	}
}