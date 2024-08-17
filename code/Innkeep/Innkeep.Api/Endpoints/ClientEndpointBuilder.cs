using Innkeep.Api.Core.Endpoints;

namespace Innkeep.Api.Endpoints;

public class ClientEndpointBuilder(string baseUri) : BaseEndpointBuilder($"{baseUri}/")
{
	public ClientEndpointBuilder WithPrint()
	{
		Endpoints.Add("print");
		return this;
	}

	public ClientEndpointBuilder Transaction()
	{
		Endpoints.Add("transaction");
		return this;
	}

	public ClientEndpointBuilder Transfer()
	{
		Endpoints.Add("transfer");
		return this;
	}
	
	public ClientEndpointBuilder WithIdentifier(string identifier)
	{
		Parameters.Add("identifier", identifier);
		return this;
	}
		
	public string Build(bool appendSlash = false)
	{
		return BuildInternal(false);
	}
}