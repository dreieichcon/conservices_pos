using Innkeep.Api.Models.Fiskaly.Objects.Transaction;
using Innkeep.Api.Models.Internal;
using Innkeep.Api.Models.Pretix.Objects.Order;

namespace Innkeep.Services.Server.Interfaces.Transaction;

public interface ITransactionService
{
	public Task<string?> CreateFromOrder(PretixOrderResponse pretixOrder, FiskalyTransaction fiskalyTransaction, ClientTransaction transaction, string receiptJson);
}