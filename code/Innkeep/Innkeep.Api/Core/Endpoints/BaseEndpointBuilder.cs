namespace Innkeep.Api.Core.Endpoints;

public abstract class BaseEndpointBuilder(string baseUri)
{
	protected readonly List<string> Endpoints = new();

	public BaseEndpointBuilder WithEndpoint(string endpoint)
	{
		Endpoints.Add(endpoint.Replace("/", ""));
		return this;
	}

	protected string BuildInternal(bool appendSlash = true)
	{
		var uri = baseUri + string.Join("/", Endpoints);

		if (appendSlash)
			uri += "/";

		return uri;
	}
}