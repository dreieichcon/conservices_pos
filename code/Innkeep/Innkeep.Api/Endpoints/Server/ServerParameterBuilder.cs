using Lite.Http.Interfaces;

namespace Innkeep.Api.Endpoints.Server;

public class ServerParameterBuilder(ServerUrlBuilder urlBuilder) : IParameterBuilder<ServerParameterBuilder>
{
	public Dictionary<string, string> Values { get; set; } = [];

	public IUrlBuilder<ServerParameterBuilder> UrlBuilder { get; set; } = urlBuilder;

	public IUrlBuilder<ServerParameterBuilder> Build()
		=> UrlBuilder;

	public ServerParameterBuilder Identifier(string identifier)
	{
		Values["identifier"] = identifier;
		return this;
	}
}