using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Internal.Interfaces.Server.Checkin;

public interface ICheckinRepository
{
	public Task<IHttpResponse<PretixCheckinResponse>> CheckIn(string secret);
}