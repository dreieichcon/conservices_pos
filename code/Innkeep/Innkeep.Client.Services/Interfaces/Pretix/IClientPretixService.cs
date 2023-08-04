using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Data.Pretix.Models;

namespace Innkeep.Client.Services.Interfaces.Pretix;

public interface IClientPretixService
{
	public Task Reload();
	
	public PretixOrganizer? SelectedOrganizer { get; set; }
	
	public PretixEvent? SelectedEvent { get; set; }
	
	public IEnumerable<PretixSalesItem> SalesItems { get; set; }

	public event EventHandler ItemUpdated;
}