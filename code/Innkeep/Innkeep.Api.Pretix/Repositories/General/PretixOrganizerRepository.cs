using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Models.Pretix.Response;
using Innkeep.Api.Pretix.Interfaces.General;
using Innkeep.Api.Pretix.Repositories.Core;
using Lite.Http.Interfaces;
using Lite.Http.Response;

namespace Innkeep.Api.Pretix.Repositories.General;

public class PretixOrganizerRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixOrganizerRepository
{
	public async Task<IHttpResponse<IEnumerable<PretixOrganizer>>> GetOrganizers()
	{
		var uri = PretixUrlBuilder.Endpoints.Organizers();

		var result = await Get<PretixResponse<PretixOrganizer>>(uri);

		return HttpResponse<IEnumerable<PretixOrganizer>>.FromResult(result, x => x.Results);
	}
}