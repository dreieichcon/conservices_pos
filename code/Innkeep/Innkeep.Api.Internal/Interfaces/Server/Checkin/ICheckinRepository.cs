using Demolite.Http.Interfaces;
using Innkeep.Api.Models.Pretix.Objects.Checkin;

namespace Innkeep.Api.Internal.Interfaces.Server.Checkin;

public interface ICheckinRepository
{
	public Task<IHttpResponse<PretixCheckinResponse>> CheckIn(string secret);
}