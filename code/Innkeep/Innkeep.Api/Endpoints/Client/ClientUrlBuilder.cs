using Lite.Http.Builder;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Endpoints.Client;

public class ClientUrlBuilder : AbstractUrlBuilder<ClientUrlBuilder, ClientParameterBuilder>,
								IUrlBuilder<ClientParameterBuilder>
{
	private string _baseUrl = string.Empty;

	public ClientUrlBuilder()
	{
		Parameters = new ClientParameterBuilder(this);
	}

	public ClientUrlBuilder Reload => AddSegment("reload");

	public ClientUrlBuilder Print => AddSegment("print");

	public ClientUrlBuilder Transaction => AddSegment("transaction");

	public ClientUrlBuilder Transfer => AddSegment("transfer");

	public override string BaseUrl => _baseUrl;

	public ClientUrlBuilder ClientAddress(string address)
	{
		_baseUrl = address;
		PathSegments.Add("client");
		return this;
	}

	private ClientUrlBuilder AddSegment(string segment)
	{
		PathSegments.Add(segment);
		return this;
	}
}