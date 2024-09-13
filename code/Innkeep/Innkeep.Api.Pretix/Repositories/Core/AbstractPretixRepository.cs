using System.Text.Json;
using Flurl.Http;
using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Json;
using Lite.Http.Repository;

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
		request.Headers.Add("Authorization", $"Token {Token}");
		request.Headers.Add("accept", "application/json");
	}

	protected override void AttachPostHeaders(IFlurlRequest request)
		=> AttachGetHeaders(request);

	protected override void SetupClient()
		=> FlurlHttp
			.ConfigureClientForUrl(PretixUrlBuilder.Endpoints.BaseUrl)
			.WithTimeout(Timeout)
			.AllowHttpStatus("*");
}