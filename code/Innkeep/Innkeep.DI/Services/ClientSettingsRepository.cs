using System.Text.Json;
using Innkeep.Client.Data.DomainModels;
using Innkeep.Client.Interfaces.Services;

namespace Innkeep.DI.Services;

public class ClientSettingsRepository : IClientSettingsRepository
{
	private const string storage = "./config.json";
	public ClientSetting Get()
	{
		if (!File.Exists(storage))
		{
			Save(new ClientSetting());
		}

		var text = File.ReadAllText(storage);
		return JsonSerializer.Deserialize<ClientSetting>(text)!;
	}

	public void Save(ClientSetting setting)
	{
		File.WriteAllText(storage, JsonSerializer.Serialize(setting));
	}
}