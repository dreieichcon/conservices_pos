using Innkeep.Server.Data.Models;

namespace Innkeep.Api.Fiskaly.Interfaces;

public interface IFiskalyApiSettingsService
{
	public FiskalyApiSettings ApiSettings { get; set; }
	
	public bool AuthenticationSuccessful { get; set; }

	public void Read();

	public bool Save();

	public string? GetToken();

}