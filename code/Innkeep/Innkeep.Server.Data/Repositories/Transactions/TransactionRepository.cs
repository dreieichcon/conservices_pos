using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Repositories;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
	public new bool Create(Transaction item, DbContext? db = null)
	{
		using var context = InnkeepServerContext.Create();

		context.Attach(item.Organizer);
		context.Attach(item.Event);
		context.Attach(item.Device);

		return base.Create(item, context);
	}
}