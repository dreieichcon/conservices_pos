using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Api.Pretix.Interfaces.Checkin;
using Innkeep.Api.Pretix.Repositories.Core;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

namespace Innkeep.Api.Pretix.Repositories.Checkin;

public class PretixCheckinListListRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixCheckinListRepository
{
	public async Task<IHttpResponse<PretixCheckinList>> GetAll(string organizerSlug, string eventSlug)
	{
		var uri = new PretixEndpointBuilder()
				.WithOrganizer(organizerSlug)
				.WithEvent(eventSlug)
				.WithCheckinList()
				.Build();

		var response = await Get(uri);

		var result = DeserializePretixResult<PretixCheckinList>(response);

		return HttpResponse<PretixCheckinList>.FromResponse(result, x => x.Results.FirstOrDefault());
	}
}