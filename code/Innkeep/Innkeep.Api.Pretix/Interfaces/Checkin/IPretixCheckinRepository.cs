using Demolite.Http.Interfaces;
using Innkeep.Api.Models.Pretix.Objects.Checkin;

namespace Innkeep.Api.Pretix.Interfaces.Checkin;

public interface IPretixCheckinRepository
{
	public Task<IHttpResponse<PretixCheckinResponse>> CheckIn(string organizerSlug, PretixCheckin checkin);
}