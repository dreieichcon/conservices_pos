using Demolite.Http.Interfaces;
using Innkeep.Api.Models.Pretix.Objects.Checkin;

namespace Innkeep.Api.Pretix.Interfaces.Checkin;

public interface IPretixCheckinListRepository
{
	public Task<IHttpResponse<IEnumerable<PretixCheckinList>>> GetAll(string organizerSlug, string eventSlug);
}