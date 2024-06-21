using Innkeep.Client.Services.Legacy.Models;

namespace Innkeep.Client.Services.Legacy.Interfaces.File;

public interface IClientSettingsService
{
	public ClientSetting Setting { get; set; }

	public void Save();

	public void Load();
}