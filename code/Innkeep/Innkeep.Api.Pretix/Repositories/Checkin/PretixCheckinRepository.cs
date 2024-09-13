using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Api.Models.Pretix.Response;
using Innkeep.Api.Pretix.Interfaces.Checkin;
using Innkeep.Api.Pretix.Repositories.Core;
using Lite.Http.Interfaces;
using Lite.Http.Response;

namespace Innkeep.Api.Pretix.Repositories.Checkin;

public class PretixCheckinRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixCheckinRepository
{
	public async Task<IHttpResponse<PretixCheckinResponse>> CheckIn(string organizerSlug, PretixCheckin checkin)
	{
		var uri = PretixUrlBuilder.Endpoints.Organizer(organizerSlug).Checkin();

		var result = await Post<PretixCheckin, PretixResponse<PretixCheckinResponse>>(uri, checkin);

		return HttpResponse<PretixCheckinResponse>.FromResult(result, x => x.Results.FirstOrDefault());
	}
}