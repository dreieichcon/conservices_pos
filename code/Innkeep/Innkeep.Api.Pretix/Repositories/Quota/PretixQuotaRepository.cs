using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.Sales;
using Innkeep.Api.Pretix.Interfaces.Quota;
using Innkeep.Api.Pretix.Interfaces.Sales;
using Innkeep.Api.Pretix.Repositories.Core;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

namespace Innkeep.Api.Pretix.Repositories.Quota;

public class PretixQuotaRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixQuotaRepository
{
	public async Task<IHttpResponse<IEnumerable<PretixQuota>>> GetAll(string organizerSlug, string eventSlug)
	{
		var uri = new PretixEndpointBuilder()
				.WithOrganizer(organizerSlug)
				.WithEvent(eventSlug)
				.WithQuotas()
				.WithAvailability()
				.Build();

		var response = await Get(uri);

		var result = DeserializePretixResult<PretixQuota>(response);

		return HttpResponse<IEnumerable<PretixQuota>>.FromResponse(result, x => x.Results);
	}
}