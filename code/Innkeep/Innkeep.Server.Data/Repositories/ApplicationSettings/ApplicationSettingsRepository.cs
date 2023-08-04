using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Data.Interfaces.ApplicationSettings;
using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Data.Repositories;

public class ApplicationSettingsRepository : BaseRepository<ApplicationSetting>, IApplicationSettingsRepository
{
	public ApplicationSetting GetSetting()
	{
		var item = Get();

		if (item is not null) return item;
		
		Create(new ApplicationSetting());

		return Get()!;
	}
}