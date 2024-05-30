using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories;

public class PretixOrganizerRepository(IPretixAuthenticationService authenticationService)
	: BasePretixRepository<PretixOrganizer>(authenticationService), IPretixOrganizerRepository
{
	public async Task<IEnumerable<PretixOrganizer>> GetOrganizers()
	{
		var uri = new PretixEndpointBuilder().WithOrganizers().Build();

		var content = await Get(uri);

		if (content is null)
			return new List<PretixOrganizer>();

		var deserialized = Deserialize(content);

		return deserialized is not null ? deserialized.Results : new List<PretixOrganizer>();
	}
}