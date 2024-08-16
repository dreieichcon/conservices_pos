using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Checkin;

namespace Innkeep.Services.Server.Interfaces.Pretix;

public interface IPretixCheckinService
{
	public Task<PretixCheckinResponse?> CheckIn(CheckinRequest request);
}