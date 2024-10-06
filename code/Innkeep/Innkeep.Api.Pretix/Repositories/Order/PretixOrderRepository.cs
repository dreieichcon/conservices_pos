using Demolite.Http.Interfaces;
using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Helpers.Pretix;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Order;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.Order;

public class PretixOrderRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixOrderRepository
{
	public async Task<IHttpResponse<PretixOrderResponse>> CreateOrder(
		string organizerSlug,
		string eventSlug,
		IEnumerable<DtoSalesItem> cart,
		bool isTestMode
	)
	{
		var uri = PretixUrlBuilder.Endpoints.Organizer(organizerSlug).Event(eventSlug).Orders();

		var payload = PretixOrderHelper.CreateOrder(cart, isTestMode);

		return await Post<PretixOrder, PretixOrderResponse>(uri, payload);
	}
}