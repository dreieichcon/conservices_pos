using Innkeep.Endpoints.Base;

namespace Innkeep.Endpoints.Fiskaly;

public class FiskalyTransactionEndpointBuilder : BaseEndpointBuilder
{
	private List<string> Parameters { get; set; } = new();
	
	public FiskalyTransactionEndpointBuilder() 
		: base("https://kassensichv-middleware.fiskaly.com/api/v2/")
	{
		
	}
	
	public FiskalyTransactionEndpointBuilder WithTss(string tssId)
	{
		Endpoints.Add("tss");
		Endpoints.Add(tssId);
		return this;
	}

	public FiskalyTransactionEndpointBuilder WithTransaction(string transactionId)
	{
		Endpoints.Add("tx");
		Endpoints.Add(transactionId);
		return this;
	}
	
	public FiskalyTransactionEndpointBuilder WithRevision(string revision)
	{
		Parameters.Add($"tx_revision={revision}");

		return this;
	}
	
	public new string Build()
	{
		var endpoint = Endpoint + string.Join("/", Endpoints);
		
		if (Parameters.Any()) endpoint += "?" + string.Join('&', Parameters);

		return endpoint;
	}

	
}