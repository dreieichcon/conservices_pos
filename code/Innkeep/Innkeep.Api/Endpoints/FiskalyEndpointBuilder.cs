using Innkeep.Api.Core.Endpoints;

namespace Innkeep.Api.Endpoints;

public class FiskalyEndpointBuilder() : BaseEndpointBuilder("https://kassensichv-middleware.fiskaly.com/api/v2")
{
	public FiskalyEndpointBuilder Authenticate()
	{
		Endpoints.Add("auth");
		return this;
	}
}