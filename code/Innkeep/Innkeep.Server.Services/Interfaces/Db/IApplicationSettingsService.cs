using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Services.Interfaces.Db;

public interface IApplicationSettingsService
{
	public ApplicationSetting ActiveSetting { get; set; }

	public void UpdateSetting(PretixOrganizer pretixOrganizer, PretixEvent pretixEvent, string organizerInfo);

	public void Save();
}