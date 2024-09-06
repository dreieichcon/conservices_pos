using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.General;

public class PretixEventRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository<PretixEvent>(authenticationService), IPretixEventRepository
{
	public async Task<IEnumerable<PretixEvent>> GetEvents(PretixOrganizer organizer)
	{
		var uri = new PretixEndpointBuilder().WithOrganizer(organizer).WithEvents().Build();

		return await GetEventsInternal(uri);
	}

	public async Task<IEnumerable<PretixEvent>> GetEvents(string pOrganizerSlug)
	{
		var uri = new PretixEndpointBuilder().WithOrganizer(pOrganizerSlug).WithEvents().Build();

		return await GetEventsInternal(uri);
	}

	public async Task<PretixEvent?> GetEvent(string pOrganizerSlug, string pEventSlug)
	{
		var uri = new PretixEndpointBuilder().WithOrganizer(pOrganizerSlug).WithEvent(pEventSlug).Build();

		var response = await Get(uri);

		return DeserializeOrNull<PretixEvent>(response);
	}

	public async Task<PretixEventSettings?> GetEventSettings(string organizerSlug, string eventSlug)
	{
		var uri = new PretixEndpointBuilder().WithOrganizer(organizerSlug).WithEvent(eventSlug).WithSettings().Build();

		var response = await Get(uri);

		return DeserializeOrNull<PretixEventSettings>(response);
	}

	private async Task<IEnumerable<PretixEvent>> GetEventsInternal(string uri)
	{
		var response = await Get(uri);

		if (!response.IsSuccess)
			return new List<PretixEvent>();

		var result = Deserialize(response.Content);

		return result is not null ? result.Results : new List<PretixEvent>();
	}
}