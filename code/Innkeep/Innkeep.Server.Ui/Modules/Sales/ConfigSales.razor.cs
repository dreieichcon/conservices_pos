using Innkeep.Api.Models.Internal;
using Innkeep.Server.Services.Interfaces;
using Innkeep.Server.Services.Interfaces.Pretix;
using Microsoft.AspNetCore.Components;

namespace Innkeep.Server.Ui.Modules.Sales;

public partial class ConfigSales
{
	[Inject]
	public IPretixSalesItemService SalesItemService { get; set; } = null!;

	public IEnumerable<DtoSalesItem> SalesItems => SalesItemService.DtoSalesItems;

	protected override async Task OnInitializedAsync()
	{
		SalesItemService.SalesItemsUpdated += async (_, _) => await InvokeAsync(StateHasChanged);
	}
}