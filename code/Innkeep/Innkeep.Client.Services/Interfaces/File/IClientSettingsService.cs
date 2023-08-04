using Innkeep.Client.Services.Models;

namespace Innkeep.Client.Services.Interfaces.File;

public interface IClientSettingsService
{
	public ClientSetting Setting { get; set; }

	public void Save();

	public void Load();
}