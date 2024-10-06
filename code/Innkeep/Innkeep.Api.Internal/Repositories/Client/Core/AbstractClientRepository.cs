using System.Text.Json;
using Demolite.Http.Interfaces;
using Demolite.Http.Repository;
using Flurl.Http;
using Innkeep.Api.Endpoints.Client;
using Innkeep.Api.Json;

namespace Innkeep.Api.Internal.Repositories.Client.Core;

public class AbstractClientRepository : AbstractHttpRepository<ClientParameterBuilder>
{
	protected override bool DeserializeIfError => false;

	protected override IFlurlRequest CreateRequest(IUrlBuilder<ClientParameterBuilder> urlBuilder)
	{
		var request = base.CreateRequest(urlBuilder);
		request.WithTimeout(TimeSpan.FromMilliseconds(Timeout));
		request.AllowAnyHttpStatus();
		request.EnsureClient();
		request.Client.HttpClient.DefaultRequestHeaders.Accept.Clear();
		return request;
	}

	protected override JsonSerializerOptions GetOptions()
		=> SerializerOptions.GetServerOptions();

	protected override void AttachGetHeaders(IFlurlRequest request)
		=> request.Headers.Add("Accept", "*/*");

	protected override void AttachPostHeaders(IFlurlRequest request)
		=> AttachGetHeaders(request);

	protected override Task PrepareRequest()
		=> Task.CompletedTask;

	protected override void SetupClient()
	{
		// do nothing here, as the request is dependent on the url passed to the builder
	}
}