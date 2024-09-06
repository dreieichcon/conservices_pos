using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Http.Interfaces;

namespace Innkeep.Api.Pretix.Interfaces.Checkin;

public interface IPretixCheckinRepository
{
	public Task<IHttpResponse<PretixCheckinResponse>> CheckIn(string organizerSlug, PretixCheckin checkin);
}