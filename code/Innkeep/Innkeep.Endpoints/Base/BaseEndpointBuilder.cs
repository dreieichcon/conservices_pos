namespace Innkeep.Endpoints.Base;

public abstract class BaseEndpointBuilder
{
	private readonly string _endpoint;
	protected readonly List<string> Endpoints = new();

	protected BaseEndpointBuilder(string endpoint)
	{
		_endpoint = endpoint;
	}
	
	public BaseEndpointBuilder WithEndpoint(string endpoint)
	{
		Endpoints.Add(endpoint.Replace("/", ""));
		return this;
	}

	public string Build()
	{
		return _endpoint + string.Join("/", Endpoints) + "/";
	}
}