using Innkeep.Client.Services.Legacy.Models;

namespace Innkeep.Client.Services.Legacy.Interfaces.File;

public interface IClientSettingsRepository
{
	public ClientSetting Get();

	public void Save(ClientSetting setting);
}