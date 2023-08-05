using Innkeep.Api.Fiskaly.Interfaces;
using Innkeep.Server.Data.Interfaces.Fiskaly;
using Innkeep.Server.Data.Models;

namespace Innkeep.Server.Services.Services.Db;

public class FiskalyApiSettingsService : IFiskalyApiSettingsService
{
	private readonly IFiskalyApiSettingsRepository _apiSettingsRepository;

	public FiskalyApiSettingsService(IFiskalyApiSettingsRepository apiSettingsRepository)
	{
		_apiSettingsRepository = apiSettingsRepository;
		Read();
	}
	
	public required FiskalyApiSettings ApiSettings { get; set; }

	public bool AuthenticationSuccessful { get; set; }

	public void Read()
	{
		ApiSettings = _apiSettingsRepository.GetOrCreate();
	}

	public bool Save()
	{
		return _apiSettingsRepository.Update(ApiSettings);
	}

	public string? GetToken() => ApiSettings.Token;
}