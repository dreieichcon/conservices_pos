using Innkeep.Api.Pretix.Legacy.Models.Objects;

namespace Innkeep.Client.Services.Interfaces.Server;

public interface IClientPretixRepository
{
	public Task<PretixOrganizer> GetOrganizer();

	public Task<PretixEvent> GetEvent();

	public Task<IEnumerable<PretixSalesItem>> GetSalesItems();

	public Task<IEnumerable<PretixCheckinList>> GetCheckinLists();
}