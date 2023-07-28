using Innkeep.Client.Interfaces.Services;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.DI.Services.Client.Core;

public class ClientPretixService : IClientPretixService
{
	private readonly IClientPretixRepository _clientPretixRepository;

	public ClientPretixService(IClientPretixRepository clientPretixRepository)
	{
		_clientPretixRepository = clientPretixRepository;
		Task.Run(Reload);
	}

	public async Task Reload()
	{
		SelectedOrganizer = await _clientPretixRepository.GetOrganizer();
		SelectedEvent = await _clientPretixRepository.GetEvent();
		SalesItems = await _clientPretixRepository.GetSalesItems();
	}
	
	public PretixOrganizer? SelectedOrganizer { get; set; }

	public PretixEvent? SelectedEvent { get; set; }

	public IEnumerable<PretixSalesItem> SalesItems { get; set; } = new List<PretixSalesItem>();

	public event EventHandler? ItemUpdated;
}