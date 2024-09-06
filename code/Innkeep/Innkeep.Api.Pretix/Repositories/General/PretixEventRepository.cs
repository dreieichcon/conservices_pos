using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Pretix.Interfaces.General;
using Innkeep.Api.Pretix.Repositories.Core;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

namespace Innkeep.Api.Pretix.Repositories.General;

public class PretixEventRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixEventRepository
{
	public async Task<IHttpResponse<IEnumerable<PretixEvent>>> GetEvents(PretixOrganizer organizer)
	{
		var uri = new PretixEndpointBuilder().WithOrganizer(organizer).WithEvents().Build();

		return await GetEventsInternal(uri);
	}

	public async Task<IHttpResponse<IEnumerable<PretixEvent>>> GetEvents(string pretixOrganizerSlug)
	{
		var uri = new PretixEndpointBuilder().WithOrganizer(pretixOrganizerSlug).WithEvents().Build();

		return await GetEventsInternal(uri);
	}

	public async Task<IHttpResponse<PretixEvent>> GetEvent(string pOrganizerSlug, string pEventSlug)
	{
		var uri = new PretixEndpointBuilder().WithOrganizer(pOrganizerSlug).WithEvent(pEventSlug).Build();

		var response = await Get(uri);

		var result = DeserializePretixResult<PretixEvent>(response);

		return HttpResponse<PretixEvent>.FromResponse(result, x => x.Results.FirstOrDefault());
	}

	public async Task<IHttpResponse<PretixEventSettings>> GetEventSettings(string organizerSlug, string eventSlug)
	{
		var uri = new PretixEndpointBuilder().WithOrganizer(organizerSlug).WithEvent(eventSlug).WithSettings().Build();

		var response = await Get(uri);

		var result = DeserializePretixResult<PretixEventSettings>(response);

		return HttpResponse<PretixEventSettings>.FromResponse(result, x => x.Results.FirstOrDefault());
	}

	private async Task<HttpResponse<IEnumerable<PretixEvent>>> GetEventsInternal(string uri)
	{
		var response = await Get(uri);

		var result = DeserializePretixResult<PretixEvent>(response);

		return HttpResponse<IEnumerable<PretixEvent>>.FromResponse(result, x => x.Results);
	}
}