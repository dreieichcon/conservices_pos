using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Interfaces.Repository.Auth;
using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.Auth;

public class PretixAuthRepository(IPretixAuthenticationService authenticationService) 
	: BasePretixRepository<PretixEvent>(authenticationService), IPretixAuthRepository
{
	public async Task<bool> Authenticate()
	{
		var uri = new PretixEndpointBuilder().WithOrganizers().Build();
		var result = await Get(uri);
		return result.IsSuccess;
	}
}
