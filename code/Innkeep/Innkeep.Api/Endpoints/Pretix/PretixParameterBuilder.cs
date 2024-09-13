using Lite.Http.Interfaces;

namespace Innkeep.Api.Endpoints.Pretix;

public class PretixParameterBuilder(PretixUrlBuilder pretixUrlBuilder) : IParameterBuilder<PretixParameterBuilder>
{
	public Dictionary<string, string> Values { get; set; } = [];

	public IUrlBuilder<PretixParameterBuilder> UrlBuilder { get; set; } = pretixUrlBuilder;

	public IUrlBuilder<PretixParameterBuilder> Build()
		=> UrlBuilder;
}