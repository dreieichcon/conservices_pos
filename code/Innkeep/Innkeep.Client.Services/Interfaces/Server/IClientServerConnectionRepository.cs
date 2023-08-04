using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Models.Printer;
using Innkeep.Models.Transaction;

namespace Innkeep.Client.Services.Interfaces.Server;

public interface IClientServerConnectionRepository
{
	public Task<bool> TestConnection(Uri uri);

	public Task<bool> RegisterToServer();

	public Task<PretixOrganizer> GetOrganizer();

	public Task<PretixEvent> GetEvent();

	public Task<IEnumerable<PretixSalesItem>> GetSalesItems();

	public Task<Receipt?> SendTransaction(PretixTransaction transaction);
}