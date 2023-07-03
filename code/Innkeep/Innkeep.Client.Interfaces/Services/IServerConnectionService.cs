using Innkeep.Data.Pretix.Models;

namespace Innkeep.Client.Interfaces.Services;

public interface IServerConnectionService
{
	public string ServerEndpoint { get; set; }
	
	public IEnumerable<PretixSalesItem> SalesItems { get; set; }
	public PretixEvent CurrentEvent { get; set; }
	
	public event EventHandler? ItemUpdated;

	public void RetrieveItems();

	public void FindServerEndpoint();
}