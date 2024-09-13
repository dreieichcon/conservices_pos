using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Helpers.Pretix;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Order;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories.Core;
using Lite.Http.Interfaces;
using Lite.Http.Response;

namespace Innkeep.Api.Pretix.Repositories.Order;

public class PretixOrderRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixOrderRepository
{
	public async Task<IHttpResponse<PretixOrderResponse>> CreateOrder(
		string pretixOrganizer,
		string pretixEvent,
		IEnumerable<DtoSalesItem> cart,
		bool isTestMode
	)
	{
		var endpoint = new PretixEndpointBuilder()
						.WithOrganizer(pretixOrganizer)
						.WithEvent(pretixEvent)
						.WithOrders()
						.Build();

		var payload = PretixOrderHelper.CreateOrder(cart, isTestMode);

		var serialized = Serialize(payload);

		var response = await Post(endpoint, serialized);

		var result = DeserializePretixResult<PretixOrderResponse>(response);

		return HttpResponse<PretixOrderResponse>.FromResult(result, x => x.Results.FirstOrDefault());
	}
}