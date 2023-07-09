using Innkeep.Server.Data.Models;
using Innkeep.Server.Interfaces.Services;

namespace Innkeep.Server.Data.Interfaces;

public interface IApplicationSettingsRepository : IBaseRepository<ApplicationSetting>
{
	public ApplicationSetting GetSetting();
}