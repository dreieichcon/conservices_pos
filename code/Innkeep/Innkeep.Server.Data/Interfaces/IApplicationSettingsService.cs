using Innkeep.Data.Pretix.Models;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Interfaces;

public interface IApplicationSettingsService
{
	public ApplicationSetting ActiveSetting { get; set; }

	public void UpdateSetting(PretixOrganizer pretixOrganizer, PretixEvent pretixEvent);

	public void Save(DbContext db);
}