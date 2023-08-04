using Innkeep.Server.Data.Context;
using Innkeep.Server.Data.Interfaces;
using Innkeep.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Server.Data.Repositories;

public class CashFlowRepository : BaseRepository<CashFlow>, ICashFlowRepository
{
	public new bool Create(CashFlow item, DbContext? db = null)
	{
		using var context = InnkeepServerContext.Create();

		context.Attach(item.Event);
		context.Attach(item.Event.Organizer);
		context.Attach(item.Register);

		return base.Create(item, context);
	}
}