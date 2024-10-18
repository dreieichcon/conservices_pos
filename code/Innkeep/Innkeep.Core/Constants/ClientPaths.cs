namespace Innkeep.Core.Constants;

public static class ClientPaths
{
	public const string DataDirectory = "../clientdata/";

	public const string EnvDirectory = DataDirectory + "env/";

	public const string EnvFilePath = EnvDirectory + "app.env";

	public const string DatabaseDirectory = DataDirectory + "db/";

	public const string DatabasePath = DatabaseDirectory + "client.db";

	public const string CertDirectory = DataDirectory + "cert/";

	public const string CertPath = CertDirectory + "cert.pfx";

	public const string LogFilePath = DataDirectory + "log/log.txt";
}