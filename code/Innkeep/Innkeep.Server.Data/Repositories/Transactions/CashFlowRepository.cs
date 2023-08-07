using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces.Transactions;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Repositories.Transactions;

public class CashFlowRepository : BaseRepository<CashFlow>, ICashFlowRepository
{
	public new bool Create(CashFlow item)
	{
		using var context = InnkeepServerContext.Create();

		context.Attach(item.Event);
		context.Attach(item.Event.Organizer);
		context.Attach(item.Register);

		var set = GetDbSetFromContext(context);

		set.Add(item);

		return TrySave(context);
	}
}