using Innkeep.Api.Models.Internal;
using Innkeep.Api.Server.Interfaces;
using Innkeep.Services.Client.Interfaces.Internal;
using Innkeep.Services.Client.Interfaces.Pos;

namespace Innkeep.Services.Client.Pos;

public class SalesItemService : ISalesItemService
{
	private readonly ISalesItemRepository _salesItemRepository;
	private readonly IEventRouter _router;

	public SalesItemService(ISalesItemRepository salesItemRepository, IEventRouter router)
	{
		_salesItemRepository = salesItemRepository;
		_router = router;

		_router.OnRegisterConnected += async (_, _) => await Load();
	}

	public event EventHandler? ItemsUpdated;

	public IList<DtoSalesItem> SalesItems { get; set; } = new List<DtoSalesItem>();

	public async Task Load()
	{
		SalesItems = (await _salesItemRepository.GetSalesItems()).ToList();
		_router.SalesItemsReloaded();
		ItemsUpdated?.Invoke(this, EventArgs.Empty);
	}
}