using Innkeep.Api.Models.Pretix.Objects.General;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixEventRepository
{
	public Task<IEnumerable<PretixEvent>> GetEvents(PretixOrganizer organizer);

	public Task<IEnumerable<PretixEvent>> GetEvents(string pOrganizerSlug);

	public Task<PretixEvent?> GetEvent(string pOrganizerSlug, string pEventSlug);

	public Task<PretixEventSettings?> GetEventSettings(string organizerSlug, string eventSlug);
}