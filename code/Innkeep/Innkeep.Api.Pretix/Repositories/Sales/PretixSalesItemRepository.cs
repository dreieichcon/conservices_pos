using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.Sales;
using Innkeep.Api.Pretix.Interfaces.Sales;
using Innkeep.Api.Pretix.Repositories.Core;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

namespace Innkeep.Api.Pretix.Repositories.Sales;

public class PretixSalesItemRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixSalesItemRepository
{
	public async Task<IHttpResponse<IEnumerable<PretixSalesItem>>> GetItems(string organizerSlug, string eventSlug)
	{
		var uri = new PretixEndpointBuilder().WithOrganizer(organizerSlug).WithEvent(eventSlug).WithItems().Build();

		var response = await Get(uri);

		var result = DeserializePretixResult<PretixSalesItem>(response);

		return HttpResponse<IEnumerable<PretixSalesItem>>.FromResponse(result, x => x.Results);
	}
}