using Lite.Http.Builder;

namespace Innkeep.Api.Endpoints.Pretix;

public class PretixUrlBuilder : AbstractUrlBuilder<PretixUrlBuilder, PretixParameterBuilder>
{
	public PretixUrlBuilder()
	{
		Parameters = new PretixParameterBuilder(this);
	}

	public override string BaseUrl => "https://api.pretix.com/v1/";

	public static PretixUrlBuilder Organizers => AddSegment("organizers");

	private static PretixUrlBuilder AddSegment(string segment)
	{
		Endpoints.PathSegments.Add(segment);

		return Endpoints;
	}
}