using Demolite.Http.Interfaces;

namespace Innkeep.Api.Endpoints.Pretix;

public class PretixParameterBuilder(PretixUrlBuilder pretixUrlBuilder) : IParameterBuilder<PretixParameterBuilder>
{
	public Dictionary<string, string> Values { get; set; } = [];

	public IUrlBuilder<PretixParameterBuilder> UrlBuilder { get; set; } = pretixUrlBuilder;

	public IUrlBuilder<PretixParameterBuilder> Build()
		=> UrlBuilder;

	public PretixParameterBuilder WithAvailability(string value)
		=> AddParameter("with_availability", value);

	private PretixParameterBuilder AddParameter(string parameter, string value)
	{
		Values.TryAdd(parameter, value);

		return this;
	}
}