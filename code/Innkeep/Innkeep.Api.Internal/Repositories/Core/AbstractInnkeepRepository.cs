using System.Text;
using System.Text.Json;
using Innkeep.Api.Json;
using Innkeep.Http.Repository;

namespace Innkeep.Api.Internal.Repositories.Core;

public class AbstractInnkeepRepository : BaseHttpRepository
{
	protected override JsonSerializerOptions GetOptions() => SerializerOptions.GetServerOptions();

	protected override Task PrepareRequest() => Task.CompletedTask;

	protected override HttpContent CreatePostMessage(string content) =>
		new StringContent(content, Encoding.UTF8, "application/json");

	protected override void InitializeGetHeaders(HttpRequestMessage message)
	{
		message.Headers.Add("Accept", "*/*");
	}

	protected override void InitializePostHeaders()
	{
		Client.DefaultRequestHeaders.Add("Accept", "*/*");
	}
}