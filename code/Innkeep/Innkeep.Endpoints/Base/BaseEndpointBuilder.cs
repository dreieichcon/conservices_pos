namespace Innkeep.Endpoints.Base;

public abstract class BaseEndpointBuilder
{
	protected readonly string Endpoint;
	protected readonly List<string> Endpoints = new();

	protected BaseEndpointBuilder(string endpoint)
	{
		Endpoint = endpoint;
	}
	
	public BaseEndpointBuilder WithEndpoint(string endpoint)
	{
		Endpoints.Add(endpoint.Replace("/", ""));
		return this;
	}

	public string Build()
	{
		return Endpoint + string.Join("/", Endpoints) + "/";
	}
}