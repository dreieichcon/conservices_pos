using Innkeep.Server.Data.Interfaces.Fiskaly;
using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Data.Repositories.Fiskaly;

public class FiskalyApiSettingsRepository : BaseRepository<FiskalyApiSettings>, IFiskalyApiSettingsRepository
{
	public FiskalyApiSettings GetOrCreate()
	{
		var setting = Get();

		if (setting is not null) return setting;

		var newSetting = new FiskalyApiSettings()
		{
			TseId = string.Empty,
			ClientId = string.Empty
		};

		Create(newSetting);

		return Get()!;
	}
}