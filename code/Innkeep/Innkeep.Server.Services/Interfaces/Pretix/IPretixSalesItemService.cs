using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Sales;

namespace Innkeep.Server.Services.Interfaces.Pretix;

public interface IPretixSalesItemService
{
	public event EventHandler? SalesItemsUpdated;
	
	public IEnumerable<PretixSalesItem> SalesItems { get; set; }
	
	public IEnumerable<DtoSalesItem> DtoSalesItems { get; set; }

	public Task Load();
}