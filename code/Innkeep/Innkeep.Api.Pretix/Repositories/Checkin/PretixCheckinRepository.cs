using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Api.Pretix.Interfaces.Checkin;
using Innkeep.Api.Pretix.Repositories.Core;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

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

		return HttpResponse<PretixCheckinResponse>.FromResponse(result, x => x.Results.FirstOrDefault());
	}
}