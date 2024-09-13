using Innkeep.Api.Models.Pretix.Objects.General;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Pretix.Interfaces.General;

public interface IPretixOrganizerRepository
{
	public Task<IHttpResponse<IEnumerable<PretixOrganizer>>> GetOrganizers();
}