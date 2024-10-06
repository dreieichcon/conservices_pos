using Demolite.Http.Builder;

namespace Innkeep.Api.Endpoints.Pretix;

public class PretixUrlBuilder : AbstractUrlBuilder<PretixUrlBuilder, PretixParameterBuilder>
{
	public PretixUrlBuilder()
	{
		Parameters = new PretixParameterBuilder(this);
	}

	public override string BaseUrl => "https://pretix.eu/api/v1/";

	public PretixUrlBuilder Organizers()
		=> AddSegment("organizers");

	public PretixUrlBuilder Events()
		=> AddSegment("events");

	public PretixUrlBuilder Settings()
		=> AddSegment("settings");

	public PretixUrlBuilder Items()
		=> AddSegment("items");

	public PretixUrlBuilder Quotas()
		=> AddSegment("quotas");

	public PretixUrlBuilder CheckinLists()
		=> AddSegment("checkinlists");

	public PretixUrlBuilder Orders()
		=> AddSegment("orders");

	public PretixUrlBuilder Checkin()
		=> AddSegment("checkinrpc").AddSegment("redeem");

	public PretixUrlBuilder Organizer(string organizerSlug)
		=> AddSegment("organizers").AddSegment(organizerSlug);

	public PretixUrlBuilder Event(string eventSlug)
		=> AddSegment("events").AddSegment(eventSlug);

	private PretixUrlBuilder AddSegment(string segment)
	{
		PathSegments.Add(segment);
		return this;
	}
}