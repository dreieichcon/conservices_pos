using System.Text.Json;
using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Fiskaly.Interfaces.Auth;
using Innkeep.Api.Fiskaly.Repositories.Core;
using Innkeep.Api.Models.Fiskaly.Request;
using Innkeep.Api.Models.Fiskaly.Response;

namespace Innkeep.Api.Fiskaly.Repositories.Auth;

public class FiskalyAuthRepository(IFiskalyAuthenticationService authenticationService)
	: BaseFiskalyRepository(authenticationService), IFiskalyAuthRepository
{
	public async Task<FiskalyTokenResponse?> Authenticate()
	{
		var request = new FiskalyTokenRequest()
		{
			Key = AuthenticationService.AuthenticationInfo.Key,
			Secret = AuthenticationService.AuthenticationInfo.Secret,
		};

		var endpoint = new FiskalyEndpointBuilder().Authenticate().Build();

		var content = JsonSerializer.Serialize(request);
		
		var result = await Post(endpoint, content);

		return !result.IsSuccess ? null : Deserialize<FiskalyTokenResponse>(result.Content);
	}
	
	// Since this repository is there to update the token, the token update call is skipped
	protected override async Task PrepareRequest() 
		=> await Task.CompletedTask;

	protected override void InitializePostHeaders()
	{
		// do nothing here, as we have no token yet
	}
}