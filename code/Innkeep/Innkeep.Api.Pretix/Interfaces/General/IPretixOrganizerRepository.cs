using Demolite.Http.Interfaces;
using Innkeep.Api.Models.Pretix.Objects.General;

namespace Innkeep.Api.Pretix.Interfaces.General;

public interface IPretixOrganizerRepository
{
	public Task<IHttpResponse<IEnumerable<PretixOrganizer>>> GetOrganizers();
}