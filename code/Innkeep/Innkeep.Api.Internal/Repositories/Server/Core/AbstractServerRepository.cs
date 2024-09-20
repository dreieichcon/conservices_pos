using System.Text.Json;
using Flurl.Http;
using Innkeep.Api.Endpoints.Server;
using Innkeep.Api.Json;
using Innkeep.Db.Client.Models;
using Innkeep.Services.Interfaces;
using Lite.Http.Interfaces;
using Lite.Http.Repository;

namespace Innkeep.Api.Internal.Repositories.Server.Core;

public class AbstractServerRepository(IDbService<ClientConfig> clientConfigService)
	: AbstractHttpRepository<ServerParameterBuilder>
{
	protected string Identifier => clientConfigService.CurrentItem!.HardwareIdentifier;

	protected override bool DeserializeIfError => false;

	protected async Task<string> GetAddress()
	{
		if (clientConfigService.CurrentItem is null)
			await clientConfigService.Load();

		return clientConfigService.CurrentItem!.ServerAddress;
	}

	protected override IFlurlRequest CreateRequest(IUrlBuilder<ServerParameterBuilder> urlBuilder)
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