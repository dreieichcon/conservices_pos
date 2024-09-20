using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Api.Models.Pretix.Response;
using Innkeep.Api.Pretix.Interfaces.Checkin;
using Innkeep.Api.Pretix.Repositories.Core;
using Lite.Http.Interfaces;
using Lite.Http.Response;

namespace Innkeep.Api.Pretix.Repositories.Checkin;

public class PretixCheckinListListRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixCheckinListRepository
{
	public async Task<IHttpResponse<IEnumerable<PretixCheckinList>>> GetAll(string organizerSlug, string eventSlug)
	{
		var uri = PretixUrlBuilder.Endpoints.Organizer(organizerSlug).Event(eventSlug).CheckinLists();

		var result = await Get<PretixResponse<PretixCheckinList>>(uri);

		return HttpResponse<IEnumerable<PretixCheckinList>>.FromResult(result, x => x.Results);
	}
}