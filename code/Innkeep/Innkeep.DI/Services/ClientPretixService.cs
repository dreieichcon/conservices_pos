using Innkeep.Client.Interfaces.Services;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.DI.Services;

public class ClientPretixService : IClientPretixService
{
	public PretixOrganizer? SelectedOrganizer { get; set; }

	public PretixEvent? SelectedEvent { get; set; }

	public IEnumerable<PretixSalesItem> SalesItems { get; set; } = new List<PretixSalesItem>();

	public event EventHandler? ItemUpdated;
}