using Innkeep.Api.Models.Pretix.Objects.Sales;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixQuotaRepository
{
	public Task<List<PretixQuota>> GetAll(string organizerSlug, string eventSlug);
}