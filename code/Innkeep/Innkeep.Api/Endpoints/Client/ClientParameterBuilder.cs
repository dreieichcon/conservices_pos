using Demolite.Http.Interfaces;

namespace Innkeep.Api.Endpoints.Client;

public class ClientParameterBuilder(ClientUrlBuilder urlBuilder) : IParameterBuilder<ClientParameterBuilder>
{
	public Dictionary<string, string> Values { get; set; } = [];

	public IUrlBuilder<ClientParameterBuilder> UrlBuilder { get; set; } = urlBuilder;

	public IUrlBuilder<ClientParameterBuilder> Build()
		=> UrlBuilder;

	public ClientParameterBuilder Identifier(string identifier)
	{
		Values["identifier"] = identifier;
		return this;
	}
}