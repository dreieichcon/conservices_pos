using Innkeep.Api.Core.Endpoints;

namespace Innkeep.Api.Endpoints;

public class ServerEndpointBuilder(string baseUri) : BaseEndpointBuilder($"{baseUri}/")
{
	public ServerEndpointBuilder WithItems()
	{
		Endpoints.Add("items");
		return this;
	}

	public ServerEndpointBuilder GetAll()
	{
		Endpoints.Add("get");
		return this;
	}
	
	public string Build(bool appendSlash = false)
	{
		return BuildInternal(false);
	}
}