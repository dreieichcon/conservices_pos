using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces.Transactions;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Repositories.Transactions;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
	public new bool Create(Transaction item)
	{
		using var context = InnkeepServerContext.Create();

		context.Attach(item.Organizer);
		context.Attach(item.Event);
		context.Attach(item.Device);

		var set = GetDbSetFromContext(context);

		set.Add(item);

		return TrySave(context);
	}
}