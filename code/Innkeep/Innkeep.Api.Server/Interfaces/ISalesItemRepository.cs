using Innkeep.Api.Models.Internal;

namespace Innkeep.Api.Server.Interfaces;

public interface ISalesItemRepository
{
	public Task<IList<DtoSalesItem>> GetSalesItems();
}