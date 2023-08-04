using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Data.Interfaces.Fiskaly;

public interface IFiskalyApiSettingsRepository : IBaseRepository<FiskalyApiSettings>
{
	public FiskalyApiSettings GetOrCreate();
}