using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Pretix.Interfaces.General;
using Innkeep.Api.Pretix.Repositories.Core;
using Innkeep.Http.Interfaces;
using Innkeep.Http.Response;

namespace Innkeep.Api.Pretix.Repositories.General;

public class PretixOrganizerRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixOrganizerRepository
{
	public async Task<IHttpResponse<IEnumerable<PretixOrganizer>>> GetOrganizers()
	{
		var uri = new PretixEndpointBuilder().WithOrganizers().Build();

		var response = await Get(uri);

		var result = DeserializePretixResult<PretixOrganizer>(response);

		return HttpResponse<IEnumerable<PretixOrganizer>>.FromResponse(result, x => x.Results);
	}
}