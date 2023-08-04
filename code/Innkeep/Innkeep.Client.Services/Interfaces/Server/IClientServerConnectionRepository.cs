using Innkeep.Api.Pretix.Models.Objects;
using Innkeep.Data.Pretix.Models;
using Innkeep.Models.Printer;
using Innkeep.Models.Transaction;

namespace Innkeep.Api.Client.Interfaces;

public interface IClientServerConnectionRepository
{
	public Task<bool> TestConnection(Uri uri);

	public Task<bool> RegisterToServer();

	public Task<PretixOrganizer> GetOrganizer();

	public Task<PretixEvent> GetEvent();

	public Task<IEnumerable<PretixSalesItem>> GetSalesItems();

	public Task<Receipt?> SendTransaction(PretixTransaction transaction);
}