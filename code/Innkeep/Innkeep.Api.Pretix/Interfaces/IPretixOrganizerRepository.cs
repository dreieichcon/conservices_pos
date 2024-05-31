using Innkeep.Api.Interfaces.Repository.Core;
using Innkeep.Api.Models.Pretix.Objects.General;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixOrganizerRepository : IPretixRepository<PretixOrganizer>
{
	public Task<IEnumerable<PretixOrganizer>> GetOrganizers();
}