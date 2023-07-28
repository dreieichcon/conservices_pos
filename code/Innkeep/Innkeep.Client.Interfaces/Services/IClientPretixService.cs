using Innkeep.Data.Pretix.Models;

namespace Innkeep.Client.Interfaces.Services;

public interface IClientPretixService
{
	public PretixOrganizer? SelectedOrganizer { get; set; }
	
	public PretixEvent? SelectedEvent { get; set; }
	
	public IEnumerable<PretixSalesItem> SalesItems { get; set; }

	public event EventHandler ItemUpdated;
}