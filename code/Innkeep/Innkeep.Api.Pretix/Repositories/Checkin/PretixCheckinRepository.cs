using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.Checkin;

public class PretixCheckinRepository(IPretixAuthenticationService authenticationService)
	: AbstractPretixRepository<PretixCheckinResponse>(authenticationService), IPretixCheckinRepository
{
	public async Task<PretixCheckinResponse?> CheckIn(string organizerSlug, PretixCheckin checkin)
	{
		var uri = new PretixEndpointBuilder().WithOrganizer(organizerSlug).WithCheckin().Build();

		var payload = Serialize(checkin);

		var response = await Post(uri, payload);

		return DeserializeOrNull<PretixCheckinResponse>(response, forceDeserializeError: true);
	}
}