using Innkeep.Api.Pretix.Legacy.Models.Objects;

namespace Innkeep.Client.Services.Interfaces.Pretix;

public interface IClientPretixService
{
	public Task Reload();
	
	public PretixOrganizer? SelectedOrganizer { get; set; }
	
	public PretixEvent? SelectedEvent { get; set; }
	
	public IEnumerable<PretixSalesItem> SalesItems { get; set; }
	
	public IEnumerable<PretixCheckinList> CheckinLists { get; set; }
	
	public int SelectedCheckinList { get; set; }

	public event EventHandler ItemUpdated;
}