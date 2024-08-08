using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Innkeep.Api.Auth;
using Innkeep.Api.Core.Http;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Json;
using Innkeep.Api.Models.Fiskaly.Request.Auth;

namespace Innkeep.Api.Fiskaly.Repositories.Core;

public class BaseFiskalyRepository(IFiskalyAuthenticationService authenticationService) : BaseHttpRepository
{
	protected IFiskalyAuthenticationService AuthenticationService => authenticationService;
	protected override HttpContent CreatePostMessage(string content)
		=> new StringContent(content, Encoding.UTF8, "application/json");

	protected override HttpContent CreatePutMessage(string content) => CreatePostMessage(content);

	protected override HttpContent CreatePatchMessage(string content) => CreatePostMessage(content);

	protected override void InitializeGetHeaders(HttpRequestMessage message)
	{
		message.Headers.Add("Accept", "application/json");
		message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationService.AuthenticationInfo.Token);
	}

	protected override void InitializePostHeaders()
	{
		Client.DefaultRequestHeaders.Authorization 
			= new AuthenticationHeaderValue("Bearer", AuthenticationService.AuthenticationInfo.Token);
	}

	protected override void InitializePutHeaders() => InitializePostHeaders();

	protected override void InitializePatchHeaders() => InitializePostHeaders();

	protected override async Task PrepareRequest() 
		=> await authenticationService.GetOrUpdateToken();
	
	protected override JsonSerializerOptions GetOptions()
	{
		return new JsonSerializerOptions()
		{
			Converters =
			{
				new PretixDecimalJsonConverter(),
				new FiskalyDateTimeJsonConverter(),
				new FiskalyLongJsonConverter(),
				new JsonStringEnumConverter(),
			},
		};
	}
	
	protected async Task<bool> AuthenticateAdmin(string tssId)
	{
		var endpoint = new FiskalyEndpointBuilder()
						.WithSpecificTss(tssId)
						.WithAdminAuth()
						.Build();

		var content = Serialize(
			new FiskalyAdminAuthenticationRequest()
			{
				AdminPin = authenticationService.CurrentConfig.TseAdminPassword!,
			}
		);

		var result = await Post(endpoint, content);

		return result.IsSuccess;
	}

	protected async Task LogoutAdmin(string tssId)
	{
		var endpoint = new FiskalyEndpointBuilder()
						.WithSpecificTss(tssId)
						.WithAdminLogout()
						.Build();

		await Post(endpoint, "{}");
	}
}