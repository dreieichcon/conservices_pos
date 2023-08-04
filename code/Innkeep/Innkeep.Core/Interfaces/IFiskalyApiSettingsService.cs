using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Data.Interfaces.Fiskaly;

public interface IFiskalyApiSettingsService
{
	public FiskalyApiSettings ApiSettings { get; set; }
	
	public bool AuthenticationSuccessful { get; set; }

	public void Read();

	public void Save();
}