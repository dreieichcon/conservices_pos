namespace Innkeep.Api.Core.Endpoints;

public abstract class BaseEndpointBuilder(string baseUri)
{
	protected readonly List<string> Endpoints = new();

	protected readonly Dictionary<string, string> Parameters = new();

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

		if (Parameters.Count != 0)
		{
			uri += "?" + string.Join("&", Parameters.Select(x => $"{x.Key}={x.Value}"));
		}
		
		return uri;
	}
}