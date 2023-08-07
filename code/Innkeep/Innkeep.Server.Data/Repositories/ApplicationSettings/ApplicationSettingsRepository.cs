using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces.ApplicationSettings;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Repositories.ApplicationSettings;

public class ApplicationSettingsRepository : BaseRepository<ApplicationSetting>, IApplicationSettingsRepository
{
	public ApplicationSetting GetSetting()
	{
		using var db = InnkeepServerContext.Create();

		var item = db.ApplicationSettings.FirstOrDefault();
		if (item is not null) return item;
		
		Create(new ApplicationSetting());

		return db.ApplicationSettings.Include(x => x.SelectedEvent).Include(x => x.SelectedOrganizer).FirstOrDefault()!;
	}
}