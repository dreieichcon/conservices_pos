using Innkeep.Client.Data.DomainModels;

namespace Innkeep.Client.Interfaces.Services;

public interface IClientSettingsService
{
	public ClientSetting Setting { get; set; }

	public void Save();

	public void Load();
}