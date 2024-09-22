using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Api.Pretix.Interfaces.Checkin;
using Innkeep.Api.Pretix.Repositories.Core;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Pretix.Repositories.Checkin;

public class PretixCheckinRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixCheckinRepository
{
	public async Task<IHttpResponse<PretixCheckinResponse>> CheckIn(string organizerSlug, PretixCheckin checkin)
	{
		var uri = PretixUrlBuilder.Endpoints.Organizer(organizerSlug).Checkin();
		return await Post<PretixCheckin, PretixCheckinResponse>(uri, checkin);
	}
}