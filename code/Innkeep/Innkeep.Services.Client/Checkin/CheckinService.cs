using Innkeep.Api.Internal.Interfaces.Server.Checkin;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Services.Client.Interfaces.Checkin;

namespace Innkeep.Services.Client.Checkin;

public class CheckinService(ICheckinRepository checkinRepository) : ICheckinService
{
	public LinkedList<PretixCheckinResponse> LastCheckins { get; set; } = [];

	public async Task<PretixCheckinResponse?> CheckIn(string secret)
	{
		var result = await checkinRepository.CheckIn(secret);

		if (!result.IsSuccess)
			return null;

		if (LastCheckins.Count > 3)
			LastCheckins.RemoveLast();

		LastCheckins.AddFirst(result.Object!);
		return result.Object;
	}
}