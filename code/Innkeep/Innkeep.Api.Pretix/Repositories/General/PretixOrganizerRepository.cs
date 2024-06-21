using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.General;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.General;

public class PretixOrganizerRepository(IPretixAuthenticationService authenticationService)
	: BasePretixRepository<PretixOrganizer>(authenticationService), IPretixOrganizerRepository
{
	public async Task<IEnumerable<PretixOrganizer>> GetOrganizers()
	{
		var uri = new PretixEndpointBuilder().WithOrganizers().Build();

		var response = await Get(uri);

		if (!response.IsSuccess) return new List<PretixOrganizer>();
		
		var result = Deserialize(response.Content);

		return result is not null ? result.Results : new List<PretixOrganizer>();
	}
}