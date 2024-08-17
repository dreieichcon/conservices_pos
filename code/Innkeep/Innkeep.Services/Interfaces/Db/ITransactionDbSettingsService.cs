namespace Innkeep.Services.Interfaces.Db;

public interface ITransactionDbSettingsService
{
	public string CurrentConnectionString { get; }
	
	public string CurrentDb { get; set; }
	
	public bool DbExists { get; }
	
	public List<string> AvailableDatabases { get; set; }

	public void LoadSettings();

	public void SaveSettings();
}