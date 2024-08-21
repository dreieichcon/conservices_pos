using Innkeep.Api.Auth;
using Innkeep.Api.Endpoints;
using Innkeep.Api.Models.Pretix.Objects.Sales;
using Innkeep.Api.Pretix.Interfaces;
using Innkeep.Api.Pretix.Repositories.Core;

namespace Innkeep.Api.Pretix.Repositories.Quota;

public class PretixQuotaRepository(IPretixAuthenticationService authenticationService)
	: BasePretixRepository<PretixQuota>(authenticationService), IPretixQuotaRepository
{
	public async Task<List<PretixQuota>> GetAll(string organizerSlug, string eventSlug)
	{
		var uri = new PretixEndpointBuilder()
				.WithOrganizer(organizerSlug)
				.WithEvent(eventSlug)
				.WithQuotas()
				.WithAvailability()
				.Build();

		var response = await Get(uri);

		var deserialized = Deserialize(response.Content);

		return deserialized != null ? deserialized.Results.ToList() : [];
	}
}