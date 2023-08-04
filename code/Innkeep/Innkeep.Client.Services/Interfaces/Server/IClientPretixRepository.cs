using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.Client.Interfaces.Services;

public interface IClientPretixRepository
{
	public Task<PretixOrganizer> GetOrganizer();

	public Task<PretixEvent> GetEvent();

	public Task<IEnumerable<PretixSalesItem>> GetSalesItems();
}