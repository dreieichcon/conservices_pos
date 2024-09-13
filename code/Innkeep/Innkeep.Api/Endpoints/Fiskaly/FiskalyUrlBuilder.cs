using Lite.Http.Builder;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Endpoints.Fiskaly;

public class FiskalyUrlBuilder : AbstractUrlBuilder<FiskalyUrlBuilder, FiskalyParameterBuilder>,
								IUrlBuilder<FiskalyParameterBuilder>
{
	public FiskalyUrlBuilder()
	{
		Parameters = new FiskalyParameterBuilder(this);
	}

	public override string BaseUrl => "https://kassensichv-middleware.fiskaly.com/api/v2/";

	public FiskalyUrlBuilder Authenticate() => AddSegment("auth");

	public FiskalyUrlBuilder Tss() => AddSegment("tss");

	public FiskalyUrlBuilder Client() => AddSegment("client");

	public FiskalyUrlBuilder Client(string clientId) => Client().AddSegment(clientId);

	public FiskalyUrlBuilder Tss(string tssId) => Tss().AddSegment(tssId);

	public FiskalyUrlBuilder Admin() => AddSegment("admin");

	public FiskalyUrlBuilder AdminAuth() => Admin().Authenticate();

	public FiskalyUrlBuilder AdminLogout() => Admin().AddSegment("logout");

	public FiskalyUrlBuilder Transaction(string transactionId)
		=> AddSegment(transactionId);

	private FiskalyUrlBuilder AddSegment(string segment)
	{
		PathSegments.Add(segment);
		return this;
	}
}