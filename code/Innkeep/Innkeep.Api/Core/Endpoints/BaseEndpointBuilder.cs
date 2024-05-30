namespace Innkeep.Api.Core.Endpoints;

public abstract class BaseEndpointBuilder(string baseUri)
{
	protected readonly List<string> Endpoints = new();

	public BaseEndpointBuilder WithEndpoint(string endpoint)
	{
		Endpoints.Add(endpoint.Replace("/", ""));
		return this;
	}

	public string Build()
	{
		return baseUri + string.Join("/", Endpoints) + "/";
	}
}