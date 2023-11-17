using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Client.Services.Interfaces.Pretix;
using Innkeep.Client.Services.Interfaces.Server;

namespace Innkeep.Client.Services.Services.Pretix;

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
		CheckinLists = await _clientPretixRepository.GetCheckinLists();
	}
	
	public PretixOrganizer? SelectedOrganizer { get; set; }

	public PretixEvent? SelectedEvent { get; set; }

	public IEnumerable<PretixSalesItem> SalesItems { get; set; } = new List<PretixSalesItem>();

	public IEnumerable<PretixCheckinList> CheckinLists { get; set; } = new List<PretixCheckinList>();
	
	public int SelectedCheckinList { get; set; }

	public event EventHandler? ItemUpdated;
}