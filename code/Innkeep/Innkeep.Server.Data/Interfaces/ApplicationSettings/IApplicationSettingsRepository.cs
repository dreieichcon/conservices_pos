using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Data.Interfaces.ApplicationSettings;

public interface IApplicationSettingsRepository : IBaseRepository<ApplicationSetting>
{
	public ApplicationSetting GetSetting();
}