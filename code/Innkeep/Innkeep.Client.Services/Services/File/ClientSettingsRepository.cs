using System.Text.Json;
using Innkeep.Client.Services.Interfaces.File;
using Innkeep.Client.Services.Models;

namespace Innkeep.Client.Services.Services.File;

public class ClientSettingsRepository : IClientSettingsRepository
{
	private const string Storage = "./config.json";
	public ClientSetting Get()
	{
		if (!System.IO.File.Exists(Storage))
		{
			Save(new ClientSetting());
		}

		var text = System.IO.File.ReadAllText(Storage);
		return JsonSerializer.Deserialize<ClientSetting>(text)!;
	}

	public void Save(ClientSetting setting)
	{
		System.IO.File.WriteAllText(Storage, JsonSerializer.Serialize(setting));
	}
}