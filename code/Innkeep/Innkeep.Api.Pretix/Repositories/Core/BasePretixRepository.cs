using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Innkeep.Api.Auth;
using Innkeep.Api.Core.Http;
using Innkeep.Api.Json;
using Innkeep.Api.Models.Pretix.Response;
using Serilog;

namespace Innkeep.Api.Pretix.Repositories.Core;

public class BasePretixRepository<T>(IPretixAuthenticationService authenticationService) : BaseHttpRepository
{
	protected override async Task PrepareRequest()
	{
		if (string.IsNullOrEmpty(authenticationService.AuthenticationInfo.Token))
			authenticationService.Load();
	}

	private string Token => authenticationService.AuthenticationInfo.Token;

	protected override void InitializeGetHeaders(HttpRequestMessage message)
	{
		message.Headers.Add("Accept", "application/json");
		message.Headers.Authorization = new AuthenticationHeaderValue("Token", Token);
	}

	protected override void InitializePostHeaders()
	{
		Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", Token);
	}

	protected override void InitializePutHeaders() => throw new NotImplementedException();

	protected override void InitializePatchHeaders() => throw new NotImplementedException();

	protected override HttpContent CreatePostMessage(string content) 
		=> new StringContent(content, Encoding.UTF8, "application/json");

	protected override HttpContent CreatePutMessage(string content) => throw new NotImplementedException();
	
	protected override HttpContent CreatePatchMessage(string content) => throw new NotImplementedException();

	protected PretixResponse<T>? Deserialize(string? content)
	{
		return Deserialize<PretixResponse<T>>(content);
	}

	protected override JsonSerializerOptions GetOptions()
	{
		return new JsonSerializerOptions()
		{
			Converters =
			{
				new PretixDecimalJsonConverter(),
			},
		};
	}
}