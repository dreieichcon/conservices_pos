using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints.Pretix;
using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Models.Pretix.Response;
using Innkeep.Api.Pretix.Interfaces.Auth;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.Auth;

public class PretixAuthenticationRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository(authenticationService), IPretixAuthenticationRepository
{
	public async Task<bool> Authenticate()
	{
		var uri = PretixUrlBuilder.Endpoints.Organizers();

		var result = await Get<PretixResponse<PretixOrganizer>>(uri);

		return result.IsSuccess;
	}
}