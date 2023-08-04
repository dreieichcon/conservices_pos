using Innkeep.Client.Services.Models;

namespace Innkeep.Client.Services.Interfaces.File;

public interface IClientSettingsRepository
{
	public ClientSetting Get();

	public void Save(ClientSetting setting);
}