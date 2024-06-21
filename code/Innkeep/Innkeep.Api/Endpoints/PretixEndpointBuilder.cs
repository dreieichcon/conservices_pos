using Innkeep.Api.Core.Endpoints;
using Innkeep.Api.Models.Pretix;
using Innkeep.Api.Models.Pretix.Objects;
using Innkeep.Api.Models.Pretix.Objects.General;

namespace Innkeep.Api.Endpoints;

public class PretixEndpointBuilder() : BaseEndpointBuilder("https://pretix.eu/api/v1/")
{
	public PretixEndpointBuilder WithOrganizers()
	{
		Endpoints.Add("organizers");
		return this;
	}

	public PretixEndpointBuilder WithOrganizer(string pOrganizerSlug)
	{
		Endpoints.Add("organizers");
		Endpoints.Add(pOrganizerSlug);
		return this;
	}
	
	public PretixEndpointBuilder WithOrganizer(PretixOrganizer pOrganizer)
	{
		Endpoints.Add("organizers");
		Endpoints.Add(pOrganizer.Slug);
		return this;
	}

	public PretixEndpointBuilder WithEvents()
	{
		Endpoints.Add("events");
		return this;
	}
	
	public PretixEndpointBuilder WithEvent(string pEventSlug)
	{
		Endpoints.Add("events");
		Endpoints.Add(pEventSlug);
		return this;
	}
	
	public PretixEndpointBuilder WithEvent(PretixEvent pEvent)
	{
		Endpoints.Add("events");
		Endpoints.Add(pEvent.Slug);
		return this;
	}

	public PretixEndpointBuilder WithItems()
	{
		Endpoints.Add("items");
		return this;
	}

	public string Build()
	{
		return BuildInternal();
	}
}