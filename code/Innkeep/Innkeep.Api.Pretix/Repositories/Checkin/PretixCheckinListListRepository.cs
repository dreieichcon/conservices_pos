using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.Checkin;

public class PretixCheckinListListRepository(IPretixAuthenticationService authenticationService)
	: BasePretixRepository<PretixCheckinList>(authenticationService), IPretixCheckinListRepository
{
	public async Task<IEnumerable<PretixCheckinList>> GetAll(string organizerSlug, string eventSlug)
	{
		var uri = new PretixEndpointBuilder()
				.WithOrganizer(organizerSlug)
				.WithEvent(eventSlug)
				.WithCheckinList()
				.Build();

		var response = await Get(uri);

		if (!response.IsSuccess)
			return new List<PretixCheckinList>();

		var result = Deserialize(response.Content);

		return result != null ? result.Results : new List<PretixCheckinList>();
	}
}