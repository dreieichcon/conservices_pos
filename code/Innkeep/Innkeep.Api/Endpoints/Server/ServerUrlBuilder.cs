using Lite.Http.Builder;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Endpoints.Server;

public class ServerUrlBuilder : AbstractUrlBuilder<ServerUrlBuilder, ServerParameterBuilder>,
								IUrlBuilder<ServerParameterBuilder>
{
	private string _baseUrl = string.Empty;

	public ServerUrlBuilder()
	{
		Parameters = new ServerParameterBuilder(this);
	}

	public ServerUrlBuilder SalesItems
	{
		get
		{
			var builder = (ServerUrlBuilder)AddSegment("items");
			builder.AddSegment("get");
			return builder;
		}
	}

	public ServerUrlBuilder Transaction => (ServerUrlBuilder)AddSegment("transaction");

	public ServerUrlBuilder Create => (ServerUrlBuilder)AddSegment("create");

	public ServerUrlBuilder Register => (ServerUrlBuilder)AddSegment("register");

	public ServerUrlBuilder Connect => (ServerUrlBuilder)AddSegment("connect");

	public ServerUrlBuilder Discover => (ServerUrlBuilder)AddSegment("discover");

	public ServerUrlBuilder Checkin => (ServerUrlBuilder)AddSegment("checkin");

	public ServerUrlBuilder Entry => (ServerUrlBuilder)AddSegment("entry");

	public override string BaseUrl => _baseUrl;

	public ServerUrlBuilder Address(string address)
	{
		_baseUrl = address;
		return this;
	}
}