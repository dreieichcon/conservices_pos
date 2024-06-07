using Innkeep.Api.Pretix.Legacy.Models.Objects;
using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Services.Legacy.Interfaces.Db;

public interface IApplicationSettingsService
{
	public ApplicationSetting ActiveSetting { get; set; }

	public void UpdateSetting(PretixOrganizer pretixOrganizer, PretixEvent pretixEvent, string organizerInfo);

	public void Save();

	public void Reload();
}