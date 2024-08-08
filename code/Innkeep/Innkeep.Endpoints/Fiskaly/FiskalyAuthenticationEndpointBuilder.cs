using Innkeep.Endpoints.Base;

namespace Innkeep.Endpoints.Fiskaly;

public class FiskalyAuthenticationEndpointBuilder : BaseEndpointBuilder
{
	public FiskalyAuthenticationEndpointBuilder() 
		: base("https://kassensichv-middleware.fiskaly.com/api/v2/")
	{
		
	}
	
	public FiskalyAuthenticationEndpointBuilder WithApiAuthentication()
	{
		Endpoints.Add("auth");
		return this;
	}
	
	public new string Build()
	{
		return Endpoint + string.Join("/", Endpoints);
	}
}