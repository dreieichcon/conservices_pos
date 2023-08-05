using System.Text;
using System.Text.Json;
using Innkeep.Api.Fiskaly.Interfaces;
using Innkeep.Api.Fiskaly.Models;
using Innkeep.Core.Core;
using Innkeep.Endpoints.Fiskaly;
using Serilog;

namespace Innkeep.Api.Fiskaly.Repositories;

public class FiskalyAuthenticationRepository : BaseHttpRepository, IFiskalyAuthenticationRepository
{
	private readonly IFiskalyApiSettingsService _fiskalySettingsService;

	public FiskalyAuthenticationRepository(IFiskalyApiSettingsService fiskalySettingsService)
	{
		_fiskalySettingsService = fiskalySettingsService;
	}
	
	protected override void PrepareGetHeaders(HttpRequestMessage message)
	{
		// do nothing
	}

	protected override void PreparePostHeaders()
	{
		// do nothing
	}

	protected override void PreparePutHeaders()
	{
		// do nothing
	}

	public async Task<AuthenticateApiResponseModel?> AuthenticateApi()
	{
		var endpoint = new FiskalyAuthenticationEndpointBuilder().WithApiAuthentication().Build();

		var json = new AuthenticateApiRequestModel()
		{
			ApiSecret = _fiskalySettingsService.ApiSettings.Secret!,
			ApiKey = _fiskalySettingsService.ApiSettings.Key!,
		}.ToJson();

		var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

		try
		{
			var response = await ExecutePostRequest(endpoint, jsonContent);

			var deserialized = JsonSerializer.Deserialize<AuthenticateApiResponseModel>(response);
			if (deserialized is not null) return deserialized;
		}
		catch (Exception ex)
		{
			Log.Debug("Received null response for Authentication against Fiskaly: {Exception}", ex);
		}
		return null;
	}
}