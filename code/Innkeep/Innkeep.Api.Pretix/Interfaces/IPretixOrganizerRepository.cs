using Innkeep.Api.Interfaces.Repository;
using Innkeep.Api.Interfaces.Repository.Core;
using Innkeep.Api.Models.Pretix;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixOrganizerRepository : IPretixRepository<PretixOrganizer>
{
	public Task<IEnumerable<PretixOrganizer>> GetOrganizers();
}