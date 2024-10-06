using System.Text.Json;
using Demolite.Http.Repository;
using Flurl.Http;
using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Json;

namespace Innkeep.Api.Pretix.Repositories.Core;

public abstract class AbstractPretixRepository(IPretixAuthenticationService authenticationService)
	: AbstractHttpRepository<PretixParameterBuilder>
{
	private string Token => authenticationService.AuthenticationInfo.Token;

	protected override bool DeserializeIfError => false;

	protected override Task PrepareRequest()
	{
		if (string.IsNullOrEmpty(authenticationService.AuthenticationInfo.Token))
			authenticationService.Load();

		return Task.CompletedTask;
	}

	protected override JsonSerializerOptions GetOptions()
		=> SerializerOptions.GetOptionsForPretix();

	protected override void AttachGetHeaders(IFlurlRequest request)
	{
		FlurlHttp.GetClientForRequest(request).Headers.Add("Authorization", $"Token {Token}");

		request.WithHeader("Authorization", $"Token {Token}");
		request.WithHeader("Accept", "application/json");
	}

	protected override void AttachPostHeaders(IFlurlRequest request)
		=> AttachGetHeaders(request);

	protected override void SetupClient()
	{
		try
		{
			FlurlHttp
				.ConfigureClientForUrl(PretixUrlBuilder.Endpoints.BaseUrl)
				.WithTimeout(TimeSpan.FromMilliseconds(Timeout))
				.AllowHttpStatus("*")
				.Build()
				.HttpClient.DefaultRequestHeaders.Accept.Clear();
		}
		catch (ArgumentException ex)
		{
			// do nothing, as the client was already configured at a different point
		}
	}
}