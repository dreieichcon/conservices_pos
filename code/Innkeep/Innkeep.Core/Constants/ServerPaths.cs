namespace Innkeep.Core.Constants;

public static class ServerPaths
{
	public const string DataDirectory = "../serverdata/";

	public const string EnvDirectory = DataDirectory + "env/";

	public const string EnvFilePath = EnvDirectory + "app.env";

	public const string DatabaseDirectory = DataDirectory + "db/";

	public const string DatabasePath = DatabaseDirectory + "server.db";

	public const string TransactionDatabaseDirectory = DatabaseDirectory + "transactions/";

	public const string ConfigDirectory = DataDirectory + "config/";

	public const string CertDirectory = DataDirectory + "cert/";

	public const string CertPath = CertDirectory + "cert.pfx";

	public const string LogFilePath = DataDirectory + "log/log.txt";
}