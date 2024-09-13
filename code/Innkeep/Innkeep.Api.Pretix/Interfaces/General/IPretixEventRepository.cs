using Innkeep.Api.Models.Pretix.Objects.General;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Pretix.Interfaces.General;

public interface IPretixEventRepository
{
	public Task<IHttpResponse<IEnumerable<PretixEvent>>> GetEvents(PretixOrganizer organizer);

	public Task<IHttpResponse<IEnumerable<PretixEvent>>> GetEvents(string pretixOrganizerSlug);

	public Task<IHttpResponse<PretixEvent>> GetEvent(string pOrganizerSlug, string pEventSlug);

	public Task<IHttpResponse<PretixEventSettings>> GetEventSettings(string organizerSlug, string eventSlug);
}