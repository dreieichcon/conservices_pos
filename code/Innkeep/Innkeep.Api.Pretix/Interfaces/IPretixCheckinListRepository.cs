using Innkeep.Api.Models.Pretix.Objects.Checkin;

namespace Innkeep.Api.Pretix.Interfaces;

public interface IPretixCheckinListRepository
{
	public Task<IEnumerable<PretixCheckinList>> GetAll(string organizerSlug, string eventSlug);

}