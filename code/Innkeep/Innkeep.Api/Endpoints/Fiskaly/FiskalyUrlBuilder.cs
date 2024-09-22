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

	public FiskalyUrlBuilder Authenticate => (FiskalyUrlBuilder)AddSegment("auth");

	public FiskalyUrlBuilder Tss => (FiskalyUrlBuilder)AddSegment("tss");

	public FiskalyUrlBuilder Client => (FiskalyUrlBuilder)AddSegment("client");

	public FiskalyUrlBuilder Admin => (FiskalyUrlBuilder)AddSegment("admin");

	public FiskalyUrlBuilder Transaction => (FiskalyUrlBuilder)AddSegment("tx");

	public FiskalyUrlBuilder AdminAuth => Admin.Authenticate;

	public FiskalyUrlBuilder AdminLogout => (FiskalyUrlBuilder)Admin.AddSegment("logout");

	public override string LastPathSegment => "";

	public override string BaseUrl => "https://kassensichv-middleware.fiskaly.com/api/v2/";

	public FiskalyUrlBuilder SpecificClient(string clientId) => (FiskalyUrlBuilder)Client.AddSegment(clientId);

	public FiskalyUrlBuilder SpecificTss(string tssId) => (FiskalyUrlBuilder)Tss.AddSegment(tssId);

	public FiskalyUrlBuilder SpecificTransaction(string transactionId)
		=> (FiskalyUrlBuilder)Transaction.AddSegment(transactionId);
}