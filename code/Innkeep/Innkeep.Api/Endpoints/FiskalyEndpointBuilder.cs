using Innkeep.Api.Core.Endpoints;

namespace Innkeep.Api.Endpoints;

public class FiskalyEndpointBuilder() : BaseEndpointBuilder("https://kassensichv-middleware.fiskaly.com/api/v2/")
{
	public FiskalyEndpointBuilder Authenticate()
	{
		Endpoints.Add("auth");
		return this;
	}

	public FiskalyEndpointBuilder WithTss()
	{
		Endpoints.Add("tss");
		return this;
	}

	public FiskalyEndpointBuilder WithSpecificTss(string id)
	{
		Endpoints.Add("tss");
		Endpoints.Add(id);
		return this;
	}

	public FiskalyEndpointBuilder WithAdmin()
	{
		Endpoints.Add("admin");
		return this;
	}

	public FiskalyEndpointBuilder WithAdminAuth()
	{
		Endpoints.Add("admin");
		Endpoints.Add("auth");
		return this;
	}
	
	public FiskalyEndpointBuilder WithAdminLogout()
	{
		Endpoints.Add("admin");
		Endpoints.Add("logout");
		return this;
	}

	public FiskalyEndpointBuilder WithClient()
	{
		Endpoints.Add("client");
		return this;
	}

	public FiskalyEndpointBuilder WithSpecificClient(string id)
	{
		Endpoints.Add("client");
		Endpoints.Add(id);
		return this;
	}

	public FiskalyEndpointBuilder WithSpecificTransaction(string id)
	{
		Endpoints.Add("tx");
		Endpoints.Add(id);
		return this;
	}

	public FiskalyEndpointBuilder WithTransactionRevision(int revision)
	{
		Parameters.Add("tx_revision", revision.ToString());
		return this;
	}
	
	public string Build()
	{
		return BuildInternal(false);
	}
}