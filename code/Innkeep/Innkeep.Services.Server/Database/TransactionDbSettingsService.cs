using System.Text.Json;
using Innkeep.Services.Interfaces.Db;
using Innkeep.Services.Models;

namespace Innkeep.Services.Server.Database;

public class TransactionDbSettingsService : ITransactionDbSettingsService
{
	private const string DatabaseFolderPath = "./db/transactions";
	
	private static string ConfigFolderPath => Path.Combine("./config", "transaction");

	private static string ConfigFilePath => Path.Combine(ConfigFolderPath, "transactionConfig.inn");

	public List<string> AvailableDatabases { get; set; } = [];
	
	private TransactionDbConfig Config { get; set; } = null!;
	
	public string CurrentDb { get => Config.CurrentDb; set => Config.CurrentDb = value; }

	public bool DbExists => !string.IsNullOrEmpty(CurrentDb);

	public string CurrentConnectionString => DatabaseFolderPath + "/" + CurrentDb;
	

	public void LoadSettings()
	{
		if (!File.Exists(ConfigFilePath))
			CreateSettings();

		if (!Directory.Exists(DatabaseFolderPath))
			Directory.CreateDirectory(DatabaseFolderPath);
		
		var json = File.ReadAllText(ConfigFilePath);
		
		AvailableDatabases = Directory.GetFiles(DatabaseFolderPath).Where(x => x.EndsWith(".sqlite")).Select(path => Path.GetFileName(path) ?? path).ToList();
		Config = JsonSerializer.Deserialize<TransactionDbConfig>(json)!;

		var configExists = AvailableDatabases.Find(x => x == Config.CurrentDb);
		
		if (configExists is null)
			Config.CurrentDb = "";
	}

	public void SaveSettings()
	{
		var json = JsonSerializer.Serialize(Config);

		if (!Directory.Exists(ConfigFolderPath))
			Directory.CreateDirectory(ConfigFolderPath);
		
		File.WriteAllText(ConfigFilePath, json);
	}

	private void CreateSettings()
	{
		Config = new TransactionDbConfig();
		SaveSettings();
	}
}