﻿using Innkeep.Api.Models.Internal;
using Innkeep.Services.Server.Interfaces.Pretix;
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

	private async Task Reload()
	{
		await SalesItemService.Load();
	}
}