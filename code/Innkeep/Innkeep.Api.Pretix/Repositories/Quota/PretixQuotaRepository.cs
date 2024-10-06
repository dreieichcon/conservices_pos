using Demolite.Http.Interfaces;
using Demolite.Http.Response;
using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Models.Pretix.Objects.Sales;
using Innkeep.Api.Models.Pretix.Response;
using Innkeep.Api.Pretix.Interfaces.Quota;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.Quota;

public class PretixQuotaRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixQuotaRepository
{
	public async Task<IHttpResponse<IEnumerable<PretixQuota>>> GetAll(string organizerSlug, string eventSlug)
	{
		var uri = PretixUrlBuilder
				.Endpoints.Organizer(organizerSlug)
				.Event(eventSlug)
				.Quotas()
				.Parameters.WithAvailability("true")
				.Build();

		var result = await Get<PretixResponse<PretixQuota>>(uri);

		return HttpResponse<IEnumerable<PretixQuota>>.FromResult(result, x => x.Results);
	}
}