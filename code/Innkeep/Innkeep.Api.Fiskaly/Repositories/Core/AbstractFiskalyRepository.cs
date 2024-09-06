using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Innkeep.Api.Auth;
using Innkeep.Api.Json;
using Innkeep.Http.Repository;

namespace Innkeep.Api.Fiskaly.Repositories.Core;

public abstract partial class AbstractFiskalyRepository(IFiskalyAuthenticationService authenticationService)
	: BaseHttpRepository
{
	private IFiskalyAuthenticationService AuthenticationService => authenticationService;

	protected override void InitializeGetHeaders(HttpRequestMessage message)
	{
		message.Headers.Add("Accept", "application/json");

		message.Headers.Authorization = new AuthenticationHeaderValue(
			"Bearer",
			AuthenticationService.AuthenticationInfo.Token
		);
	}

	protected override void InitializePostHeaders()
	{
		Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
			"Bearer",
			AuthenticationService.AuthenticationInfo.Token
		);
	}

	protected override async Task PrepareRequest() => await authenticationService.GetOrUpdateToken();

	protected override JsonSerializerOptions GetOptions() =>
		new()
		{
			Converters =
			{
				new PretixDecimalJsonConverter(),
				new FiskalyDateTimeJsonConverter(),
				new FiskalyLongJsonConverter(),
				new JsonStringEnumConverter(new FiskalyEnumNamingPolicy()),
			},
		};
}