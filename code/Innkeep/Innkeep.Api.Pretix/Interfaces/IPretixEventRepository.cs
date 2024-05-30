using Innkeep.Api.Interfaces.Repository.Core;
using Innkeep.Api.Models.Pretix.Objects;
using Innkeep.Api.Models.Pretix.Objects.General;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixEventRepository : IPretixRepository<PretixEvent>
{
	public Task<IEnumerable<PretixEvent>> GetEvents(PretixOrganizer organizer);

	public Task<IEnumerable<PretixEvent>> GetEvents(string pOrganizerSlug);
}