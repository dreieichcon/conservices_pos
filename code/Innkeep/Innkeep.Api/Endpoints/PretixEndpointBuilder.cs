using Innkeep.Api.Core.Endpoints;
using Innkeep.Api.Models.Pretix;

namespace Innkeep.Api.Endpoints;

public class PretixEndpointBuilder() : BaseEndpointBuilder("https://pretix.eu/api/v1/")
{
	public PretixEndpointBuilder WithOrganizers()
	{
		Endpoints.Add("organizers");
		return this;
	}
	
	public PretixEndpointBuilder WithOrganizer(PretixOrganizer organizer)
	{
		Endpoints.Add("organizers");
		Endpoints.Add(organizer.Slug);
		return this;
	}
}