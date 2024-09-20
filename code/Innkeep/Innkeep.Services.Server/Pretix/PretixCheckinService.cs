using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Checkin;
using Innkeep.Api.Pretix.Interfaces.Checkin;
using Innkeep.Db.Server.Models;
using Innkeep.Services.Interfaces;
using Innkeep.Services.Server.Interfaces.Pretix;

namespace Innkeep.Services.Server.Pretix;

public class PretixCheckinService(
	IPretixCheckinRepository checkinRepository,
	IDbService<PretixConfig> pretixConfigService
) : IPretixCheckinService
{
	public async Task<PretixCheckinResponse?> CheckIn(CheckinRequest request)
	{
		var item = pretixConfigService.CurrentItem!;

		var data = new PretixCheckin
		{
			Secret = request.Secret,
			CheckinLists = GetCheckinList(),
		};

		return (await checkinRepository.CheckIn(item.SelectedOrganizerSlug!, data)).Object;
	}

	private List<int> GetCheckinList()
	{
		var lst = new List<int>();

		if (pretixConfigService.CurrentItem!.SelectedCheckinListId != null)
			lst.Add(pretixConfigService.CurrentItem.SelectedCheckinListId.Value);

		return lst;
	}
}