using Demolite.Http.Interfaces;
using Innkeep.Api.Models.Internal;

namespace Innkeep.Api.Internal.Interfaces.Server.Pos;

public interface ISalesItemRepository
{
	public Task<IHttpResponse<IEnumerable<DtoSalesItem>>> GetSalesItems();
}