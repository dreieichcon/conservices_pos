using Demolite.Db.Interfaces;
using Innkeep.Db.Server.Context;
using Innkeep.Db.Server.Models.Transaction;
using Innkeep.Db.Server.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace Innkeep.Db.Server.Repositories.Transaction;

public class TransactionRepository(IDbContextFactory<InnkeepTransactionContext> contextFactory)
	: BaseInnkeepTransactionRepository<TransactionModel>(contextFactory), IDbRepository<TransactionModel>
{
}