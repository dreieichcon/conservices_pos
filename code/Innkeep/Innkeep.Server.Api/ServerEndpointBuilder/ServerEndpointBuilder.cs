using Innkeep.Core.Core;

namespace Innkeep.Server.Api.ServerEndpointBuilder;

public class ServerEndpointBuilder : BaseEndpointBuilder
{
	public ServerEndpointBuilder(Uri uri) : base($"{uri}")
	{
	}
	
	
	
	public ServerEndpointBuilder WithRegister()
	{
		Endpoints.Add("Register");
		return this;
	}
	
	public ServerEndpointBuilder WithPretix()
	{
		Endpoints.Add("Pretix");
		return this;
	}
	
	
}