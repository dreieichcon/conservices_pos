using Innkeep.Api.Models.Pretix.Objects.Checkin;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixCheckinRepository
{
	public Task<PretixCheckinResponse?> CheckIn(string organizerSlug, PretixCheckin checkin);
}