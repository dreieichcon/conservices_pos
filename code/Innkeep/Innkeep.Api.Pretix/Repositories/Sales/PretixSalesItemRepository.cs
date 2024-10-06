using Demolite.Http.Interfaces;
using Demolite.Http.Response;
using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Models.Pretix.Objects.Sales;
using Innkeep.Api.Models.Pretix.Response;
using Innkeep.Api.Pretix.Interfaces.Sales;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.Sales;

public class PretixSalesItemRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixSalesItemRepository
{
	public async Task<IHttpResponse<IEnumerable<PretixSalesItem>>> GetItems(string organizerSlug, string eventSlug)
	{
		var uri = PretixUrlBuilder.Endpoints.Organizer(organizerSlug).Event(eventSlug).Items();

		var result = await Get<PretixResponse<PretixSalesItem>>(uri);

		return HttpResponse<IEnumerable<PretixSalesItem>>.FromResult(result, x => x.Results);
	}
}