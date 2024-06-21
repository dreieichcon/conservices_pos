using Innkeep.Api.Models.Pretix.Objects.General;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixOrganizerRepository
{
	public Task<IEnumerable<PretixOrganizer>> GetOrganizers();
}