using Innkeep.Api.Models.Pretix.Objects.Sales;
using Innkeep.Http.Interfaces;

namespace Innkeep.Api.Pretix.Interfaces.Quota;

public interface IPretixQuotaRepository
{
	public Task<IHttpResponse<IEnumerable<PretixQuota>>> GetAll(string organizerSlug, string eventSlug);
}