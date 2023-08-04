using Innkeep.Api.Pretix.Models.Objects;

namespace Innkeep.Client.Services.Interfaces.Server;

public interface IClientPretixRepository
{
	public Task<PretixOrganizer> GetOrganizer();

	public Task<PretixEvent> GetEvent();

	public Task<IEnumerable<PretixSalesItem>> GetSalesItems();
}