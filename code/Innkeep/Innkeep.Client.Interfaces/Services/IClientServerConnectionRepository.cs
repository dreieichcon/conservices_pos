using Innkeep.Core.DomainModels.Print;
using Innkeep.Data.Pretix.Models;
using Innkeep.Shared.Objects.Transaction;

namespace Innkeep.Client.Interfaces.Services;

public interface IClientServerConnectionRepository
{
	public Task<bool> TestConnection(Uri uri);

	public Task<bool> RegisterToServer();

	public Task<PretixOrganizer> GetOrganizer();

	public Task<PretixEvent> GetEvent();

	public Task<IEnumerable<PretixSalesItem>> GetSalesItems();

	public Task<Receipt?> SendTransaction(Transaction transaction);
}