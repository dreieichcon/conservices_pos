using Innkeep.Data.Pretix.Models;

namespace Innkeep.Client.Interfaces.Services;

public interface IClientServerConnectionRepository
{
	public Task<bool> TestConnection(Uri uri);

	public Task<bool> RegisterToServer();

	public Task<PretixOrganizer> GetOrganizer();

	public Task<PretixEvent> GetEvent();

	public Task<IEnumerable<PretixSalesItem>> GetSalesItems();
}