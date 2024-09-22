using System.Text.Json;
using Flurl.Http;
using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Fiskaly;
using Innkeep.Api.Json;
using Lite.Http.Repository;

namespace Innkeep.Api.Fiskaly.Repositories.Core;

public partial class AbstractFiskalyRepository(IFiskalyAuthenticationService authenticationService)
	: AbstractHttpRepository<FiskalyParameterBuilder>
{
	protected IFiskalyAuthenticationService AuthenticationService => authenticationService;

	protected override bool DeserializeIfError => true;

	protected override void SetupClient()
	{
		try
		{
			FlurlHttp
				.ConfigureClientForUrl(FiskalyUrlBuilder.Endpoints.BaseUrl)
				.AllowHttpStatus("*")
				.Build()
				.HttpClient.DefaultRequestHeaders.Accept.Clear();
		}
		catch (ArgumentException ex)
		{
			// do nothing, as the client was already configured at a different point
		}
	}

	protected override async Task PrepareRequest() => await authenticationService.GetOrUpdateToken();

	protected override JsonSerializerOptions GetOptions() => SerializerOptions.GetOptionsForFiskaly();

	protected override void AttachGetHeaders(IFlurlRequest request)
	{
		request.Headers.Add("Accept", "application/json");
		request.WithOAuthBearerToken(AuthenticationService.AuthenticationInfo.Token);
	}

	protected override void AttachPostHeaders(IFlurlRequest request)
	{
		AttachGetHeaders(request);
	}
}