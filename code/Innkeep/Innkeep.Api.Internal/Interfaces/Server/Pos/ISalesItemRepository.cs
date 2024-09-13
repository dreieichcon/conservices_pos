using Innkeep.Api.Models.Internal;
using Lite.Http.Interfaces;

namespace Innkeep.Api.Internal.Interfaces.Server.Pos;

public interface ISalesItemRepository
{
	public Task<IHttpResponse<IEnumerable<DtoSalesItem>>> GetSalesItems();
}