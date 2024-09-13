using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
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
		var uri = new PretixEndpointBuilder().WithOrganizer(organizerSlug).WithCheckin().Build();

		var payload = Serialize(checkin);

		var response = await Post(uri, payload);

		var result = DeserializePretixResult<PretixCheckinResponse>(response);

		return HttpResponse<PretixCheckinResponse>.FromResult(result, x => x.Results.FirstOrDefault());
	}
}