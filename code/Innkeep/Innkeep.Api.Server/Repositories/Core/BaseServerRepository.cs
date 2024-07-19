using System.Text.Json;
using Innkeep.Api.Core.Http;

namespace Innkeep.Api.Server.Repositories.Core;

public class BaseServerRepository : BaseHttpRepository
{
	protected override JsonSerializerOptions GetOptions() => throw new NotImplementedException();

	protected override async Task PrepareRequest()
	{
		// do nothing here
	}

	protected override HttpContent CreatePostMessage(string content) => throw new NotImplementedException();

	protected override HttpContent CreatePutMessage(string content) => throw new NotImplementedException();

	protected override HttpContent CreatePatchMessage(string content) => throw new NotImplementedException();

	protected override void InitializeGetHeaders(HttpRequestMessage message)
	{
		message.Headers.Add("Accept", "application/json");
	}

	protected override void InitializePostHeaders()
	{
		throw new NotImplementedException();
	}

	protected override void InitializePutHeaders()
	{
		throw new NotImplementedException();
	}

	protected override void InitializePatchHeaders()
	{
		throw new NotImplementedException();
	}
}
