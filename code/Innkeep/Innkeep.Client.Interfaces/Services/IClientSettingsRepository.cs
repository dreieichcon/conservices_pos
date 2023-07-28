using Innkeep.Client.Data.DomainModels;

namespace Innkeep.Client.Interfaces.Services;

public interface IClientSettingsRepository
{
	public ClientSetting Get();

	public void Save(ClientSetting setting);
}