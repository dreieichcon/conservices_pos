using Demolite.Http.Interfaces;

namespace Innkeep.Api.Endpoints.Fiskaly;

public class FiskalyParameterBuilder(FiskalyUrlBuilder urlBuilder) : IParameterBuilder<FiskalyParameterBuilder>
{
	public Dictionary<string, string> Values { get; set; } = [];

	public IUrlBuilder<FiskalyParameterBuilder> UrlBuilder { get; set; } = urlBuilder;

	public IUrlBuilder<FiskalyParameterBuilder> Build()
		=> UrlBuilder;

	public FiskalyParameterBuilder TransactionRevision(int revision)
	{
		Values.Add("tx_revision", revision.ToString());
		return this;
	}
}