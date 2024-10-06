using Demolite.Http.Interfaces;
using Demolite.Http.Response;
using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Models.Pretix.Response;
using Innkeep.Api.Pretix.Interfaces.General;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.General;

public class PretixEventRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixEventRepository
{
	public async Task<IHttpResponse<IEnumerable<PretixEvent>>> GetEvents(string organizerSlug)
	{
		var uri = PretixUrlBuilder.Endpoints.Organizer(organizerSlug).Events();

		var result = await Get<PretixResponse<PretixEvent>>(uri);

		return HttpResponse<IEnumerable<PretixEvent>>.FromResult(result, x => x.Results);
	}

	public async Task<IHttpResponse<PretixEvent>> GetEvent(string organizerSlug, string eventSlug)
	{
		var uri = PretixUrlBuilder.Endpoints.Organizer(organizerSlug).Event(eventSlug);
		return await Get<PretixEvent>(uri);
	}

	public async Task<IHttpResponse<PretixEventSettings>> GetEventSettings(string organizerSlug, string eventSlug)
	{
		var uri = PretixUrlBuilder.Endpoints.Organizer(organizerSlug).Event(eventSlug).Settings();

		var result = await Get<PretixEventSettings>(uri);

		return result;
	}
}