using Innkeep.Api.Models.Pretix.Objects.General;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Pretix.Interfaces.General;

public interface IPretixEventRepository
{
	public Task<IHttpResponse<IEnumerable<PretixEvent>>> GetEvents(string organizerSlug);

	public Task<IHttpResponse<PretixEvent>> GetEvent(string organizerSlug, string eventSlug);

	public Task<IHttpResponse<PretixEventSettings>> GetEventSettings(string organizerSlug, string eventSlug);
}