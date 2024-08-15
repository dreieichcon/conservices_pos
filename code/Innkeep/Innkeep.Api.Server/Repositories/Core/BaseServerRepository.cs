using System.Text;
using System.Text.Json;
using Innkeep.Api.Core.Http;
using Innkeep.Api.Json;

namespace Innkeep.Api.Server.Repositories.Core;

public class BaseServerRepository : BaseHttpRepository
{
	protected override JsonSerializerOptions GetOptions() => SerializerOptions.GetServerOptions();

	protected override Task PrepareRequest()
	{
		// do nothing here
		return Task.CompletedTask;
	}

	protected override HttpContent CreatePostMessage(string content) 
		=> new StringContent(content, Encoding.UTF8, "application/json");

	protected override HttpContent CreatePutMessage(string content) => throw new NotImplementedException();

	protected override HttpContent CreatePatchMessage(string content) => throw new NotImplementedException();

	protected override void InitializeGetHeaders(HttpRequestMessage message)
	{
		message.Headers.Add("Accept", "*/*");
	}

	protected override void InitializePostHeaders()
	{
		Client.DefaultRequestHeaders.Add("Accept", "*/*");
	}

	protected override void InitializePutHeaders()
	{
		// do nothing
	}

	protected override void InitializePatchHeaders()
	{
		// do nothing
	}
}
