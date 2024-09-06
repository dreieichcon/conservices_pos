using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Innkeep.Api.Auth;
using Innkeep.Api.Json;
using Innkeep.Http.Repository;

namespace Innkeep.Api.Pretix.Repositories.Core;

public abstract class AbstractPretixRepository(IPretixAuthenticationService authenticationService) : BaseHttpRepository
{
	private string Token => authenticationService.AuthenticationInfo.Token;

	protected override Task PrepareRequest()
	{
		if (string.IsNullOrEmpty(authenticationService.AuthenticationInfo.Token))
			authenticationService.Load();

		return Task.CompletedTask;
	}

	protected override void InitializeGetHeaders(HttpRequestMessage message)
	{
		message.Headers.Add("Accept", "application/json");
		message.Headers.Authorization = new AuthenticationHeaderValue("Token", Token);
	}

	protected override void InitializePostHeaders()
	{
		Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", Token);
	}

	protected override JsonSerializerOptions GetOptions() =>
		new()
		{
			Converters =
			{
				new PretixDecimalJsonConverter(),
				new JsonStringEnumConverter(),
				new JsonStringEnumConverter(new PretixEnumNamingPolicy()),
			},
		};
}