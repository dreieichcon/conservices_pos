using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Sales;

namespace Innkeep.Services.Server.Interfaces.Pretix;

public interface IPretixSalesItemService
{
	public IEnumerable<PretixSalesItem> SalesItems { get; set; }

	public IEnumerable<DtoSalesItem> DtoSalesItems { get; set; }

	public DateTime LastQuotaUpdate { get; set; }

	public DateTime LastFullUpdate { get; set; }

	public event EventHandler? SalesItemsUpdated;

	public Task Load();

	public Task LoadQuotas();

	public Task ReloadTimer();
}